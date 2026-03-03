using System.IO;
using System;
using System.Text;
using UglyToad.PdfPig;
using System.Threading.Tasks;
using OpenAI;
using OpenAI.Responses;
using System.Text.Json;

var httpClient = new HttpClient
{
    Timeout = TimeSpan.FromMinutes(60),
    BaseAddress = new Uri("https://api.openai.com/v1/"),
};
httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User)}");

var body = new    
{
    model = "gpt-5-nano",
    input = "Olá, tudo bem?"
};

var response = await httpClient.PostAsync("responses", new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
var responseContent = await response.Content.ReadAsStringAsync();
Console.WriteLine(responseContent);

// var prompt = File.ReadAllText("ArquivoLeitura/Prompt.txt").Replace("@TEXTO", text).Replace("@PERGUNTA", "quem é a analista do escopo?");

// var pdfFile = "ArquivoLeitura/Escopo.pdf";



// var response = await client.Responses.CreateAsync(new ResponseCreateRequest
// {
//     Model = "gpt-5 nano",
//     Input = "Say 'this is a test.'"
// });

// Console.WriteLine($"[ASSISTANT]: {response.OutputText()}");


// var httpClient = new HttpClient
// {
//     Timeout = TimeSpan.FromMinutes(60),
//     BaseAddress = new Uri("http://localhost:11434/")
// };
// var ollama = new OllamaApiClient(httpClient, "gpt-oss");
// string text = "";
// using (var pdf = PdfDocument.Open(pdfFile))
//     {
//         foreach (var page in pdf.GetPages())
//         {
//             text += page.Text;
//         }
//     }

// ConversationContext context = null;
// await foreach (var stream in ollama.StreamCompletion(prompt, context))
// {
//     Console.Write(stream.Response);
// }
