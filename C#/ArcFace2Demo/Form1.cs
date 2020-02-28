using Emgu.CV;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcFace2Demo
{
    public partial class Form1 : Form
    {
        #region ArcFaceConst
        const uint ASF_DETECT_MODE_VIDEO = 0x00000000;  //Video模式，一般用于多帧连续检测
        const uint ASF_DETECT_MODE_IMAGE = 0xFFFFFFFF;  //Image模式，一般用于静态图的单次检测

        const uint ASF_NONE = 0x00000000;
        const uint ASF_FACE_DETECT = 0x00000001; //此处detect可以是tracking或者detection两个引擎之一，具体的选择由detect mode 确定
        const uint ASF_FACERECOGNITION = 0x00000004;
        const uint ASF_AGE = 0x00000008;
        const uint ASF_GENDER = 0x00000010;
        const uint ASF_FACE3DANGLE = 0x00000020;

        /// <summary>
        /// 结构ASF_FaceRect的长度
        /// 32位程序是16,64位程序需要改为32
        /// </summary>
        const int SizeOfASF_FaceRect = 32;

        #endregion


        #region ArceDataStructure
        /// <summary>
        /// 人脸在图片中的位置
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct ASF_FaceRect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public Rectangle GetRectangle()
            {
                return new Rectangle(Left, Top, Right - Left, Bottom - Top);
            }
        }
        /// <summary>
        /// 多人脸信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct ASF_MultiFaceInfo
        {

            public IntPtr PFaceRect;
            public IntPtr PFaceOrient;
            [MarshalAs(UnmanagedType.I4)]
            public int FaceNum;
        }


        /// <summary>
        /// 单人脸信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct ASF_SingleFaceInfo
        {
            public ASF_FaceRect FaceRect;
            public int FaceOrient;

        }



        /// <summary>
        /// 人脸特征
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct ASF_FaceFeature
        {
            public IntPtr PFeature;
            [MarshalAs(UnmanagedType.I4)]
            public int FeatureSize;
        }


        #endregion

        #region ArcWrapper

        /// <summary>
        /// 激活SDK
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="sdkKey"></param>
        /// <returns>0:激活成功，0x16002表示已经激活</returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFActivation", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int ASFActivation(string appId, string sdkKey);

        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="detectMode">long会返回scale错误0x16004</param>
        /// <param name="orientPriority"></param>
        /// <param name="scale"></param>
        /// <param name="maxFaceNumber"></param>
        /// <param name="combinedMask"></param>
        /// <param name="pEngine"></param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFInitEngine", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int ASFInitEngine(uint detectMode, int orientPriority, int scale, int maxFaceNumber, uint combinedMask, out IntPtr pEngine);
        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="format"></param>
        /// <param name="pImageData"></param>
        /// <param name="faceInfo"></param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFDetectFaces", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int ASFDetectFaces(IntPtr pEngine, int width, int height, int format, IntPtr pImageData, out ASF_MultiFaceInfo faceInfo);

        /// <summary>
        /// 单人脸特征提取
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="format"></param>
        /// <param name="faceInfo"></param>
        /// <param name="faceFeature"></param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFFaceFeatureExtract", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int ASFFaceFeatureExtract(IntPtr pEngine, int width, int height, int format, IntPtr pImageData, ref ASF_SingleFaceInfo faceInfo, out ASF_FaceFeature faceFeature);
        /// <summary>
        /// 脸特征比对
        /// </summary>
        /// <param name="pEngine"></param>
        /// <param name="faceFeature1"></param>
        /// <param name="faceFeature2"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFFaceFeatureCompare", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int ASFFaceFeatureCompare(IntPtr pEngine, ref ASF_FaceFeature faceFeature1, ref ASF_FaceFeature faceFeature2, out float result);
        /// <summary>
        /// 销毁引擎
        /// </summary>
        /// <param name="engine"></param>
        /// <returns></returns>
        [DllImport("libarcsoft_face_engine.dll", EntryPoint = "ASFUninitEngine", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int ASFUninitEngine(IntPtr engine);
        #endregion

        /// <summary>
        /// 特征库
        /// </summary>
        IntPtr _PFeatureLib;
        /// <summary>
        /// 特征库人脸数量
        /// </summary>
        int _FeatureLibFaceCount = 0;
        /// <summary>
        /// 特征库人脸ID列表
        /// </summary>
        List<string> _FeatureLibIDList = new List<string>();

        /// <summary>
        /// 人脸特征结构
        /// </summary>
        ASF_FaceFeature _FaceFeature = new ASF_FaceFeature { FeatureSize = 1032 };

        /// <summary>
        /// 人脸识别的结果
        /// </summary>
        class FaceResult
        {
            /// <summary>
            /// 人脸框矩形
            /// </summary>
            public Rectangle Rectangle { get; set; }
            /// <summary>
            /// 人脸ID
            /// </summary>
            public string ID { get; set; }
            /// <summary>
            /// 比对结果
            /// </summary>
            public float Score { get; set; }

            public override string ToString()
            {
                return [DISCUZ_CODE_0]quot; ID: { ID}\r\n结果：{ Score}
                ";
            }
        }
        /// <summary>
        /// 多人脸识别结果集
        /// </summary>
        ConcurrentDictionary<int, FaceResult> _FaceResults = new ConcurrentDictionary<int, FaceResult>();
        /// <summary>
        /// 检测到的人脸数量
        /// </summary>
        int _DetectedFaceCount = 0;

        /// <summary>
        /// 视频捕获
        /// </summary>
        VideoCapture _VideoCapture;
        Mat _Frame = new Mat();

        /// <summary>
        /// 虹软人脸引擎
        /// </summary>
        IntPtr _PEngine = IntPtr.Zero;

        /// <summary>
        /// 比对一次总耗时
        /// </summary>
        long _TotalElapsedMilliseconds = 0;
        /// <summary>
        /// 识别任务
        /// </summary>
        Task _TaskMatch;
        /// <summary>
        /// 向识别任务发送取消指令的东东
        /// </summary>
        CancellationTokenSource _CTS = new CancellationTokenSource();

        /// <summary>
        /// 图像数据
        /// </summary>
        IntPtr _PImageData;
        /// <summary>
        /// 宽、高、图像数据长度
        /// </summary>
        int _ImageWidth, _ImageHeight, _ImageSize;
        /// <summary>
        /// 是否要保存当前人脸特征
        /// </summary>
        bool _SaveFlag = false;

        PictureBox _PictureBox;

        public Form1()
        {
            InitializeComponent();

            _PictureBox = new PictureBox();
            _PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            _PictureBox.Dock = DockStyle.Fill;
            this.Controls.Add(_PictureBox);

            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_TaskMatch != null)
            {
                _CTS.Cancel();
                while (_TaskMatch.Status == TaskStatus.Running)
                    Task.Delay(1000).Wait();
            }
            _VideoCapture.Stop();

            if (_PEngine != IntPtr.Zero)
                ASFUninitEngine(_PEngine);

            if (_PFeatureLib != IntPtr.Zero)
                Marshal.FreeCoTaskMem(_PFeatureLib);

            if (_PImageData != IntPtr.Zero)
                Marshal.FreeCoTaskMem(_PImageData);
        }

        private unsafe void Form1_Load(object sender, EventArgs e)
        {
            var ret = ASFActivation("BKgqTWQPQQbomfqvyd2VJzTUqPp3JD8zjAzDcqsL1jLa", "2nkDTmnkpS53cpSY42fFS9nEUzg8x4MDGkAubSsebtm1");
            if (ret != 0 && ret != 0x16002)
            {
                MessageBox.Show("SDK激活失败：0x" + ret.ToString("x2"));
                return;
            }
            ret = ASFInitEngine(ASF_DETECT_MODE_IMAGE, 1, 32, 10, ASF_FACE_DETECT | ASF_FACERECOGNITION, out _PEngine);
            if (ret != 0)
            {
                MessageBox.Show([DISCUZ_CODE_0]quot; 人脸识别引擎初始化失败: " + ret.ToString("x2"));
                return;
            }
            //初始化识别结果集
            for (int i = 0; i < 10; i++)
                _FaceResults[i] = new FaceResult();
            //初始化特征库
            _PFeatureLib = Marshal.AllocCoTaskMem(1032 * 1000 + 1032 * 10000 * 20);
            var bytes = File.ReadAllBytes("Feature.dat");
            var ids = File.ReadAllLines("Id.txt");
            for (int i = 0; i < 20 * 20; i++)
            {
                Marshal.Copy(bytes, 0, IntPtr.Add(_PFeatureLib, _FeatureLibFaceCount * 1032), bytes.Length);
                _FeatureLibIDList.AddRange(ids);
                _FeatureLibFaceCount += ids.Length;
            }

            _VideoCapture = new VideoCapture();


            //_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth, 1024);
            //_VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, 768);
            _VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps, 10);
            _VideoCapture.Start();

            _VideoCapture.ImageGrabbed += (object oo, EventArgs es) =>
            {
                _VideoCapture.Retrieve(_Frame, 1);
                using (Graphics g = Graphics.FromImage(_Frame.Bitmap))
                {
                    g.DrawString([DISCUZ_CODE_0]quot; 比对总耗时{ _TotalElapsedMilliseconds}
                    毫秒", this.Font, Brushes.White, 0, 0);
                    for (int i = 0; i < _DetectedFaceCount; i++)
                    {
                        if (_FaceResults.TryGetValue(i, out var faceResult))
                        {
                            g.DrawRectangle(Pens.Red, faceResult.Rectangle);
                            g.DrawString(faceResult.ToString(), this.Font, Brushes.White, faceResult.Rectangle.Location);
                        }
                    }
                }
                this._PictureBox.Image = _Frame.Bitmap;
            };

            _PictureBox.Click += (object oo, EventArgs es) =>
            {
                if (MessageBox.Show("您确定要保存人脸特征数据吗？", "确认信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    _SaveFlag = true;
            };

            _ImageSize = _VideoCapture.Width * _VideoCapture.Height * 3;
            _PImageData = Marshal.AllocCoTaskMem(_ImageSize);
            _ImageWidth = _VideoCapture.Width;
            _ImageHeight = _VideoCapture.Height;



            _TaskMatch = Task.Run(() =>
            {
                Task.Delay(1000).Wait();

                while (!_CTS.IsCancellationRequested)
                {
                    try
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Restart();

                        Marshal.Copy(_Frame.GetData(), 0, _PImageData, _ImageSize);
                        ret = ASFDetectFaces(_PEngine, _ImageWidth, _ImageHeight, 513, _PImageData, out var faceInfo);

                        if (ret != 0 || faceInfo.FaceNum == 0)
                        {
                            _DetectedFaceCount = 0;
                            continue;
                        }

                        for (int detectedFaceIndex = 0; detectedFaceIndex < faceInfo.FaceNum; detectedFaceIndex++)
                        {

                            float score = 0;
                            string id = "";
                            ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo
                            {
                                FaceRect = Marshal.PtrToStructure<ASF_FaceRect>(IntPtr.Add(faceInfo.PFaceRect, SizeOfASF_FaceRect * detectedFaceIndex)),
                                FaceOrient = 1// Marshal.ReadInt32(IntPtr.Add(faceInfo.PFaceOrient, i * 4))
                            };


                            ret = ASFFaceFeatureExtract(_PEngine, _ImageWidth, _ImageHeight, 513, _PImageData, ref singleFaceInfo, out var faceFeature);
                            if (ret != 0)
                                continue;
                            _FaceResults[detectedFaceIndex].Rectangle = singleFaceInfo.FaceRect.GetRectangle();


                            if (_SaveFlag)
                            {
                                byte[] bufferSave = new byte[1032];
                                Marshal.Copy(faceFeature.PFeature, bufferSave, 0, 1032);
                                var newId = DateTime.Now.Ticks.ToString();

                                FileStream fs = new FileStream("Feature.dat", FileMode.Append);
                                fs.Write(bufferSave, 0, 1032);
                                fs.Close();

                                var streamWriter = File.AppendText("Id.txt");
                                streamWriter.Write("\r\n" + newId);
                                streamWriter.Close();

                                Marshal.Copy(bufferSave, 0, IntPtr.Add(_PFeatureLib, 1032 * _FeatureLibFaceCount), 1032);
                                _FeatureLibIDList.Add(newId);
                                _FeatureLibFaceCount++;

                                if (detectedFaceIndex == faceInfo.FaceNum - 1)
                                {
                                    MessageBox.Show("保存特征数据成功！");
                                    _SaveFlag = false;
                                }
                                continue;
                            }



                            ConcurrentBag<int> needCompareFaceIndexs = new ConcurrentBag<int>();

                            Parallel.For(0, _FeatureLibFaceCount, faceIndex =>
                            {

                                byte* pLib = ((byte*)_PFeatureLib) + 1032 * faceIndex + 8;
                                byte* pCurrent = ((byte*)faceFeature.PFeature) + 8;
                                int count = 0;
                                for (int j = 0; j < 1024; j++)
                                {
                                    if (*pLib++ == *pCurrent++)
                                        count++;
                                }
                                if (count > 80)
                                    needCompareFaceIndexs.Add(faceIndex);
                            });

                            foreach (var index in needCompareFaceIndexs)//650ms
                            {
                                _FaceFeature.PFeature = IntPtr.Add(_PFeatureLib, index * 1032);
                                ASFFaceFeatureCompare(_PEngine, ref faceFeature, ref _FaceFeature, out var r);

                                if (r > 0.8 && r > score)
                                {
                                    score = r;
                                    id = _FeatureLibIDList[index];
                                }
                            }

                            _FaceResults[detectedFaceIndex].Score = score;
                            _FaceResults[detectedFaceIndex].ID = id;
                        }


                        _DetectedFaceCount = faceInfo.FaceNum;

                        sw.Stop();
                        _TotalElapsedMilliseconds = sw.ElapsedMilliseconds;

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }, _CTS.Token);

        }


    }
}

