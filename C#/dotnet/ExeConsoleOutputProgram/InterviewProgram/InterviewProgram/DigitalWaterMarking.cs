using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Image = System.Drawing.Image;

namespace InterviewProgram
{
    public static class DigitalWaterMarking
    {
        static void Main0(string[] args)
        {
            //set a working directory
            string WorkingDirectory = @"E:\watermarking";
            //define a string of text to use as the Copyright message
            string Copyright = "水印 水印 水印 水印";
            //create a image object containing the photograph to watermark
            Image imgPhoto = Image.FromFile(WorkingDirectory + "\\watermark_photo.jpg");
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;
            //create a Bitmap the Size of the original photograph
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            //load the Bitmap into a Graphics object
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            //create a image object containing the watermark
            Image imgWatermark = new Bitmap(/*WorkingDirectory + "\\watermark.bmp"*/imgPhoto);
            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;
            //------------------------------------------------------------
            //Step #1 - Insert Copyright message
            //------------------------------------------------------------
            //Set the rendering quality for this Graphics object
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            //Draws the photo Image object at original size to the graphics object.
            grPhoto.DrawImage(
              imgPhoto,                // Photo Image object
              new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
              0,                   // x-coordinate of the portion of the source image to draw.
              0,                   // y-coordinate of the portion of the source image to draw.
              phWidth,                // Width of the portion of the source image to draw.
              phHeight,                // Height of the portion of the source image to draw.
              GraphicsUnit.Pixel);          // Units of measure
                                            //-------------------------------------------------------
                                            //to maximize the size of the Copyright message we will
                                            //test multiple Font sizes to determine the largest posible
                                            //font we can use for the width of the Photograph
                                            //define an array of point sizes you would like to consider as possiblities
                                            //-------------------------------------------------------
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            //Loop through the defined sizes checking the length of the Copyright string
            //If its length in pixles is less then the image width choose this Font size.
            for (int i = 0; i < 7; i++)
            {
                //set a Font object to Arial (i)pt, Bold
                crFont = new Font("arial", sizes[i], FontStyle.Italic);
                //Measure the Copyright string in this Font
                crSize = grPhoto.MeasureString(Copyright, crFont);
                if ((ushort)crSize.Width < (ushort)phWidth)
                    break;
            }
            //Since all photographs will have varying heights, determine a
            //position 5% from the bottom of the image
            int yPixlesFromBottom = (int)(phHeight * .05);
            //Now that we have a point size use the Copyrights string height
            //to determine a y-coordinate to draw the string of the photograph
            float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));
            //Determine its x-coordinate by calculating the center of the width of the image
            float xCenterOfImg = (phWidth / 2);
            //Define the text layout by setting the text alignment to centered
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;
            //define a Brush which is semi trasparent black (Alpha set to 153)
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));
            //Draw the Copyright string
            grPhoto.DrawString(Copyright, //string of text
              crFont, //font
              semiTransBrush2, //Brush
              new PointF(xCenterOfImg + 1, yPosFromBottom + 1), //Position
              StrFormat);
            //define a Brush which is semi trasparent white (Alpha set to 153)
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 0, 0, 0));
            //Draw the Copyright string a second time to create a shadow effect
            //Make sure to move this text 1 pixel to the right and down 1 pixel
            grPhoto.DrawString(Copyright, //string of text
              crFont, //font
              semiTransBrush, //Brush
              new PointF(xCenterOfImg, yPosFromBottom), //Position
              StrFormat); //Text alignment
                          //------------------------------------------------------------
                          //Step #2 - Insert Watermark image
                          //------------------------------------------------------------
                          //Create a Bitmap based on the previously modified photograph Bitmap
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            //Load this Bitmap into a new Graphic Object
            Graphics grWatermark = Graphics.FromImage(bmWatermark);
            //To achieve a transulcent watermark we will apply (2) color
            //manipulations by defineing a ImageAttributes object and
            //seting (2) of its properties.
            ImageAttributes imageAttributes = new ImageAttributes();
            //The first step in manipulating the watermark image is to replace
            //the background color with one that is trasparent (Alpha=0, R=0, G=0, B=0)
            //to do this we will use a Colormap and use this to define a RemapTable
            ColorMap colorMap = new ColorMap();
            //My watermark was defined with a background of 100% Green this will
            //be the color we search for and replace with transparency
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };
            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
            //The second color manipulation is used to change the opacity of the
            //watermark. This is done by applying a 5x5 matrix that contains the
            //coordinates for the RGBA space. By setting the 3rd row and 3rd column
            //to 0.3f we achive a level of opacity
            float[][] colorMatrixElements = {
        new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
        new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
        new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
        new float[] {0.0f, 0.0f, 0.0f, 0.3f, 0.0f},
        new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}};
            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
              ColorAdjustType.Bitmap);
            //For this example we will place the watermark in the upper right
            //hand corner of the photograph. offset down 10 pixels and to the
            //left 10 pixles
            int xPosOfWm = ((phWidth - wmWidth) - 10);
            int yPosOfWm = 10;
            grWatermark.DrawImage(imgWatermark,
              new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), //Set the detination Position
              0, // x-coordinate of the portion of the source image to draw.
              0, // y-coordinate of the portion of the source image to draw.
              wmWidth, // Watermark Width
              wmHeight, // Watermark Height
              GraphicsUnit.Pixel, // Unit of measurment
              imageAttributes); //ImageAttributes Object
                                //Replace the original photgraphs bitmap with the new Bitmap
            imgPhoto = bmWatermark;
            grPhoto.Dispose();
            grWatermark.Dispose();
            //save new image to file system.
            imgPhoto.Save(WorkingDirectory + "\\watermark_final.jpg", ImageFormat.Jpeg);
            imgPhoto.Dispose();
            imgWatermark.Dispose();
        }

        static void Main(string[] args)
        {
            AddWaterMark(@"E:\watermarking\photo.jpg", @"E:\watermarking\watermark.jpg");
        }

        /// <summary>
        /// 添加水印
        /// </summary>
        /// <param name="imgPath">原图片地址</param>
        /// <param name="sImgPath">水印图片地址</param>
        /// <returns>resMsg[0] 成功,失败 </returns>
        public static string[] AddWaterMark(string imgPath, string sImgPath)
        {
            string[] resMsg = new[] { "成功", sImgPath };
            using (Image image = Image.FromFile(imgPath))
            {
                try
                {
                    Bitmap bitmap = new Bitmap(image);

                    int width = bitmap.Width, height = bitmap.Height;
                    //水印文字
                    string text = "不做任何使用";

                    Graphics g = Graphics.FromImage(bitmap);

                    g.DrawImage(bitmap, 0, 0);

                    g.InterpolationMode = InterpolationMode.High;

                    g.SmoothingMode = SmoothingMode.HighQuality;

                    g.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);

                    Font crFont = new Font("微软雅黑", 120, FontStyle.Italic);
                    SizeF crSize = new SizeF();
                    crSize = g.MeasureString(text, crFont);

                    //背景位置(去掉了. 如果想用可以自己调一调 位置.)
                    //graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 255, 255, 255)), (width - crSize.Width) / 2, (height - crSize.Height) / 2, crSize.Width, crSize.Height);

                    SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(120, 0, 0, 0));

                    //将原点移动 到图片中点
                    g.TranslateTransform(width / 2, height / 2);
                    //以原点为中心 转 -45度
                    g.RotateTransform(-45);

                    g.DrawString(text, crFont, semiTransBrush, new PointF(0, 0));

                    //保存文件
                    bitmap.Save(sImgPath, ImageFormat.Jpeg);

                }
                catch (Exception e)
                {

                    resMsg[0] = "失败";
                    resMsg[1] = e.Message;
                }
            }

            return resMsg;
        }

        #region 添加水印
        /// <summary>
        /// 添加文字水印
        /// </summary>
        /// <param name="image"></param>
        /// <param name="text"></param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="rectX">水印开始X坐标（自动扣除文字宽度）</param>
        /// <param name="rectY">水印开始Y坐标（自动扣除文字高度</param>
        /// <param name="opacity">0-255 值越大透明度越低</param>
        /// <param name="externName">文件后缀名</param>
        /// <returns></returns>
        public static Image AddTextToImg(Image image, string text, float fontSize, float rectX, float rectY, int opacity, string externName)
        {

            Bitmap bitmap = new Bitmap(image, image.Width, image.Height);

            Graphics g = Graphics.FromImage(bitmap);

            //下面定义一个矩形区域            
            float rectWidth = text.Length * (fontSize + 10);
            float rectHeight = fontSize + 10;

            //声明矩形域

            RectangleF textArea = new RectangleF(rectX - rectWidth, rectY - rectHeight, rectWidth, rectHeight);

            Font font = new Font("微软雅黑", fontSize, FontStyle.Bold); //定义字体

            Brush whiteBrush = new SolidBrush(Color.FromArgb(opacity, 193, 143, 8)); //画文字用

            g.DrawString(text, font, whiteBrush, textArea);

            MemoryStream ms = new MemoryStream();

            //保存图片
            switch (externName)
            {
                case ".jpg":
                    bitmap.Save(ms, ImageFormat.Jpeg);
                    break;
                case ".gif":
                    bitmap.Save(ms, ImageFormat.Gif);
                    break;
                case ".png":
                    bitmap.Save(ms, ImageFormat.Png);
                    break;
                default:
                    bitmap.Save(ms, ImageFormat.Jpeg);
                    break;
            }


            Image h_hovercImg = Image.FromStream(ms);

            g.Dispose();

            bitmap.Dispose();

            return h_hovercImg;

        }


        /// <summary>
        /// 添加图片水印
        /// </summary>
        /// <param name="image"></param>
        /// <param name="text"></param>
        /// <param name="rectX">水印开始X坐标（自动扣除图片宽度）</param>
        /// <param name="rectY">水印开始Y坐标（自动扣除图片高度</param>
        /// <param name="opacity">透明度 0-1</param>
        /// <param name="externName">文件后缀名</param>
        /// <returns></returns>
        public static Image AddImgToImg(Image image, Image watermark, float rectX, float rectY, float opacity, string externName)
        {

            Bitmap bitmap = new Bitmap(image, image.Width, image.Height);

            Graphics g = Graphics.FromImage(bitmap);


            //下面定义一个矩形区域      
            float rectWidth = watermark.Width + 10;
            float rectHeight = watermark.Height + 10;

            //声明矩形域
            RectangleF textArea = new RectangleF(rectX - rectWidth, rectY - rectHeight, rectWidth, rectHeight);

            Bitmap w_bitmap = ChangeOpacity(watermark, opacity);

            g.DrawImage(w_bitmap, textArea);

            MemoryStream ms = new MemoryStream();

            //保存图片
            switch (externName)
            {
                case ".jpg":
                    bitmap.Save(ms, ImageFormat.Jpeg);
                    break;
                case ".gif":
                    bitmap.Save(ms, ImageFormat.Gif);
                    break;
                case ".png":
                    bitmap.Save(ms, ImageFormat.Png);
                    break;
                default:
                    bitmap.Save(ms, ImageFormat.Jpeg);
                    break;
            }

            Image h_hovercImg = Image.FromStream(ms);

            g.Dispose();

            bitmap.Dispose();
            return h_hovercImg;

        }

        /// <summary>
        /// 改变图片的透明度
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="opacityvalue">透明度</param>
        /// <returns></returns>
        public static Bitmap ChangeOpacity(Image img, float opacityvalue)
        {

            float[][] nArray ={ new float[] {1, 0, 0, 0, 0},

                                new float[] {0, 1, 0, 0, 0},

                                new float[] {0, 0, 1, 0, 0},

                                new float[] {0, 0, 0, opacityvalue, 0},

                                new float[] {0, 0, 0, 0, 1}};

            ColorMatrix matrix = new ColorMatrix(nArray);

            ImageAttributes attributes = new ImageAttributes();

            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            Image srcImage = img;

            Bitmap resultImage = new Bitmap(srcImage.Width, srcImage.Height);

            Graphics g = Graphics.FromImage(resultImage);

            g.DrawImage(srcImage, new Rectangle(0, 0, srcImage.Width, srcImage.Height), 0, 0, srcImage.Width, srcImage.Height, GraphicsUnit.Pixel, attributes);

            return resultImage;
        }

        #endregion
    }
}
