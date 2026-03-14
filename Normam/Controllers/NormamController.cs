using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Normam.Models;
using System.Text;
using System.Text.Json;

namespace Normam.Controllers
{
    public class NormamController : Controller
    {
        private readonly HttpClient _httpClient;

        public NormamController()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(60),
                BaseAddress = new Uri("https://api.openai.com/v1/"),
            };

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User)}");
        }

        public async Task<IActionResult> GetChat(string input)
        {
            var tools = new List<ToolsModel>
            {
                new() {
                    type = "file_search",
                    vector_store_ids = ["vs_69a76273f7a88191a0cf25fd32edf0e0"],
                    max_num_results = 20
                }
            };

            var body = new
            {
                model = "gpt-5-nano",
                instructions = System.IO.File.ReadAllText("wwwroot/Prompt.txt"),
                tools,
                input
            };
            var msg = "";

            var response = await _httpClient.PostAsync("responses", new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"));
            if(!response.IsSuccessStatusCode)
                return Ok(new {content = $"Pane na comunicação com o agente."});

            var responseContent = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<ResponseModel>(responseContent);
            OutputModel output = new OutputModel();
            ContentModel content = new ContentModel();

            if (model != null)
                output = model.output.FirstOrDefault(x => x.type == "message");
            else
                return Ok(new {content = "Pane na captura da resposta."});

            if (output != null)
                content = output.content.FirstOrDefault(x => x.type == "output_text");
            else
                return Ok(new {content = "Pane na captura da resposta.."});

            if (content != null)
                msg = content.text;
            else
                return Ok(new {content = "Pane na captura da resposta..."});

            return Ok(new {content = msg});
        }
    }
}