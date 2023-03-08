// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var api = new OpenAI_API.OpenAIAPI("YOUR_API_KEY");
var question = Console.ReadLine();
var result = await api.Completions.GetCompletion(question);
Console.WriteLine(result);