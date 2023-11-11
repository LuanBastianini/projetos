using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Mvc;
using PresentesAna.Models;

namespace PresentesAna.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ListaPresentes()
    {
        var stream = System.IO.File.ReadAllText("wwwroot/dados.txt", Encoding.Default);
        // var presentes = JsonSerializer.Deserialize<List<Presentes>>(stream);
        return Content(stream);
    }

    public IActionResult Escolher(int id, string nomeConvidado)
    {
        
        var options1 = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        var stream = System.IO.File.ReadAllText("wwwroot/dados.txt", Encoding.Default);
        var presentes = JsonSerializer.Deserialize<List<Presentes>>(stream, options1)?? new List<Presentes>();

        presentes.Add(new Presentes{
            ID = id,
            NomeConvidado = nomeConvidado.ToUpper()
        });

        System.IO.File.WriteAllText("wwwroot/dados.txt", JsonSerializer.Serialize(presentes,options1));

        return Ok();
    }

    public IActionResult Remover(int id, string nomeConvidado)
    {
        var options1 = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        var stream = System.IO.File.ReadAllText("wwwroot/dados.txt", Encoding.Default);
        var presentes = JsonSerializer.Deserialize<List<Presentes>>(stream, options1)?? new List<Presentes>();
        nomeConvidado = nomeConvidado.ToUpper();

        if(!presentes.Any(x => x.ID == id && x.NomeConvidado.Equals(nomeConvidado)))
            return BadRequest("Já foi Escolhido. :/");

        presentes.Remove(presentes.Where(x => x.ID == id && x.NomeConvidado.Equals(nomeConvidado)).First());

        System.IO.File.WriteAllText("wwwroot/dados.txt", JsonSerializer.Serialize(presentes,options1));

        return Ok("Haa que Pena. :/");
    }
}
