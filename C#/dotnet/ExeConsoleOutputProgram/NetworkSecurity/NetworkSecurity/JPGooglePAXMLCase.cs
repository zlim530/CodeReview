using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Transactions;
using System.Xml;
using Haceau.Google;
using Haceau.Google.Translate;

namespace NetworkSecurity
{
    internal class JPGooglePAXMLCase
    {
        // 创建 HttpClient 实例
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            string xmlFilePath = "C:\\localcode\\MVlogQueryCheck\\JP_072101_ScrapeFile.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            // 获取所有 <Query> 标签
            XmlNodeList queryNodes = xmlDoc.SelectNodes("//Batch/Data/Query");

            string outputFile = "C:/localcode/MVlogQueryCheck/PAScrapefileCheck.txt";
            //string dir = Path.GetDirectoryName(outputFile);
            //if (!Directory.Exists(dir))
            //{
            //    Directory.CreateDirectory(dir);
            //}
            //if (File.Exists(outputFile))
            //{
            //    Console.WriteLine($"{outputFile} already exists!");
            //    return;
            //}

            Translation t = new Translation(client);

            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                // 遍历每个 <Query> 标签
                foreach (XmlNode queryNode in queryNodes)
                {
                    // 获取 <Query> 标签的 id 属性值
                    string queryId = queryNode.Attributes["id"].Value;

                    // 获取 <SerpUrl> 标签和 <Text> 标签
                    XmlNode serpUrlNode = queryNode.SelectSingleNode("SerpUrl");
                    XmlNode textNode = queryNode.SelectSingleNode("Text");

                    // 提取 <SerpUrl> 和 <Text> 的内容
                    string serpUrl = serpUrlNode?.InnerText;
                    string text = textNode?.InnerText;

                    // 输出解析结果
                    Console.WriteLine($"Query ID: {queryId}");
                    Console.WriteLine($"Text: {text}");
                    Console.WriteLine($"ja to zh-CN:");
                    //t.SourceLanguage = "ja";
                    //t.TargetLanguage = "zh-CN";
                    //Console.WriteLine($"{t.Translate($"{text}")}");

                    string Url = $"https://fanyi.sogou.com/text?keyword={text}&transfrom=ja&transto=zh-CHS&model=general";
                    HttpResponseMessage response = client.GetAsync(Url).Result;
                    var result= response.Content.ReadAsStringAsync().Result;

                    Console.WriteLine($"SerpUrl: {serpUrl}");
                    Console.WriteLine();
                }
            }


            // 替换为你的谷歌翻译 API 密钥
            //string apiKey = "YOUR_GOOGLE_TRANSLATE_API_KEY";

            //// 要翻译的日文文本
            //string japaneseText = "こにちは、世界";

            //// 谷歌翻译 API 的 URL
            //string apiUrl = $"https://translation.googleapis.com/language/translate/v2?key={apiKey}";

            //// 构建请求数据
            //var postData = new System.Collections.Specialized.NameValueCollection
            //{
            //    { "q", japaneseText },
            //    { "source", "ja" }, // 源语言为日文
            //    { "target", "zh-CN" } // 目标语言为简体中文
            //};

            //using (var client = new HttpClient())
            //{
            //    // 发送 POST 请求
            //    var response = client.PostAsync(apiUrl, new FormUrlEncodedContent(postData)).Result;

            //    // 读取响应内容
            //    var responseContent = response.Content.ReadAsStringAsync().Result;

            //    // 解析 JSON 响应，获取翻译结果
            //    // 注意：解析 JSON 响应时需要引入 Newtonsoft.Json 程序包
            //    dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
            //    string translatedText = jsonResponse.data.translations[0].translatedText;

            //    // 输出翻译结果
            //    Console.WriteLine($"原文：{japaneseText}");
            //    Console.WriteLine($"翻译：{translatedText}");
            //}

        }

    }
}
