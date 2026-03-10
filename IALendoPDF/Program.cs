using System.IO;
using System;
using System.Text;
using UglyToad.PdfPig;
using System.Threading.Tasks;
using OpenAI;
using OpenAI.Responses;
using System.Text.Json;
using System.Numerics;
using IALendoPDF;
using System.Security.Cryptography.X509Certificates;

Console.OutputEncoding = Encoding.UTF8;

var httpClient = new HttpClient
{
    Timeout = TimeSpan.FromMinutes(60),
    BaseAddress = new Uri("https://api.openai.com/v1/"),
};
httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User)}");

var input = Console.ReadLine();
var tools = new List<Tools>
{
    new() {
        type = "file_search",
        vector_store_ids = new []{"vs_69a76273f7a88191a0cf25fd32edf0e0"},
        max_num_results = 20
    }
};

var body = new    
{
    model = "gpt-5-nano",
    instructions = File.ReadAllText("ArquivoLeitura/Prompt.txt"),
    tools,
    input
};

var response = await httpClient.PostAsync("responses", new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
var responseContent = await response.Content.ReadAsStringAsync();
var model = JsonSerializer.Deserialize<ResponseDto>(responseContent);
OutputDto output = new OutputDto();
ContentDto content = new ContentDto();

if(model != null)
    output = model.output.FirstOrDefault(x => x.type == "message");
else
    Console.WriteLine("Resposta Vazia");

if(output != null)
    content = output.content.FirstOrDefault(x => x.type == "output_text");
else
    Console.WriteLine("Saida Vazia");

if(content != null)
    Console.WriteLine(content.text);
else
    Console.WriteLine("Conteudo vazio");

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
