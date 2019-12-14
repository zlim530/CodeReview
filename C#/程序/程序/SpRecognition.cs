using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeechLib;
using System.Speech.Recognition;
using System.Diagnostics;

namespace SpeakVoice
{
    public class SpRecognition
    {
        
        #region 自己变量

        private static SpeechRecognitionEngine speechRecognitionEngine = null;
        private static bool isWorking = false;
        #endregion

        public static EventHandler GetTextFromSpeech;

        #region 开始与关闭（对外接口）

        // 语音订餐初始化位置
        public static void Initialize(
            string[] Sets = null,
            RecognizeMode Mode = RecognizeMode.Single,
            string NationLanguage = "zh-CN")
        {
            try
            {
                speechRecognitionEngine =
                    createSpeechRecognitionEngine(NationLanguage);

                // 添加识别事件函数
                speechRecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(speechRecognitionEngine_SpeechRecognized);

                // load dictionary
                loadGrammar(Sets);

                // use the system's default microphone
                speechRecognitionEngine.SetInputToDefaultAudioDevice();

                // start listening
                speechRecognitionEngine.RecognizeAsync(Mode);
            }
            catch { }
        }


        public static void Start()
        {
            isWorking = true;
        }

        public static void End()
        {
            isWorking = false;
        }

        #endregion

        #region 内部函数

        // 识别语音信息是否为空，若不为空就从语音识别中获取文本
        private static void speechRecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (isWorking)
                if (GetTextFromSpeech != null)
                    GetTextFromSpeech(e.Result.Text, null);
        }

        private static SpeechRecognitionEngine
            createSpeechRecognitionEngine
            (string preferredCulture)
        {
            foreach (RecognizerInfo config in SpeechRecognitionEngine.InstalledRecognizers())
            {
                if (config.Culture.ToString() == preferredCulture)
                {
                    speechRecognitionEngine = new SpeechRecognitionEngine(config);
                    break;
                }
            }

            // if the desired culture is not found, then load default
            if (speechRecognitionEngine == null)
            {
                speechRecognitionEngine = new SpeechRecognitionEngine(SpeechRecognitionEngine.InstalledRecognizers()[0]);
            }

            return speechRecognitionEngine;
        }

        private static void loadGrammar(string[] sets)
        {
            if (sets == null)
            {
                // Create and load a dictation grammar.
                speechRecognitionEngine.LoadGrammar(new DictationGrammar());
                return;
            }
            try
            {
                Choices texts = new Choices();
                foreach (string line in sets)
                {
                    texts.Add(line.Trim());
                }
                Grammar wordsList = new Grammar
                    (new GrammarBuilder(texts));
                speechRecognitionEngine.LoadGrammar(wordsList);
            }
            catch (Exception)
            {
                // Create and load a dictation grammar.
                speechRecognitionEngine.LoadGrammar
                    (new DictationGrammar());
            }
        }


        //private string getKnownTextOrExecute(string command)
        //{
        //    try
        //    {   // use a little bit linq for our lookup list ...
        //        var cmd = words.Where(c => c.Text == command).First();

        //        if (cmd.IsShellCommand)
        //        {
        //            Process proc = new Process();
        //            proc.EnableRaisingEvents = false;
        //            proc.StartInfo.FileName = cmd.AttachedText;
        //            proc.Start();
        //            return "you just started : " + cmd.AttachedText;
        //        }
        //        else
        //        {
        //            return cmd.AttachedText;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return command;
        //    }
        // }

        #endregion


        //public static void SetSpeack()
        //{
        //    String[] sets = { "订餐", "下菜", "提交", "明", "后", "茶", "当" };
        //    if (!isInitializeSpeechToText)
        //    {
        //        SpRecognition.Initialize(
        //            null,
        //            RecognizeMode.Multiple);

        //        SpRecognition.GetTextFromSpeech += new EventHandler(GetTextFromSpeech);
        //        isInitializeSpeechToText = true;

        //    }
        //    SpRecognition.Start();
        //}

    }
    
}
