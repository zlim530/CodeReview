using OpenAI.Managers;
using OpenAI;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;

namespace CodeCheck
{
    internal class UseOpenAIAPIkeys
    {
        static async Task Main01(string[] args)
        {
            // TODO: 替换为你自己的 API 密钥
            var openaiApiKey = "sk-************************";

            OpenAIService service = new OpenAIService(new OpenAiOptions() { ApiKey = openaiApiKey });
            //await Console.Out.WriteLineAsync("Pls input you questions:");
            //var prompt = Console.ReadLine();
            
            CompletionCreateRequest createRequest = new CompletionCreateRequest()
            {

                Prompt = "写一首关于工作的诗",
                //Prompt = prompt,
                Temperature = 0.3f,
                MaxTokens = 1000
            };

            var res = await service.Completions.CreateCompletion(createRequest, Models.TextDavinciV3);

            if (res.Successful)
            {
                var ss = res.Choices.FirstOrDefault().Text;
                Console.WriteLine(ss);
            }
            else
            {
                await Console.Out.WriteLineAsync(res.Error.Message);
            }
            
        }
    }
}
