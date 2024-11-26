using Microsoft.AspNetCore.Mvc;

//                              Aula 60 Controllers e Rotas 
//                              Aula 61 Views


namespace MVCFilmes.Controllers
{
    public class FilmesController : Controller
    {
        // (1-60,61) Retornando uma action
        public IActionResult Index()
        {
            return View();
        }

        // (2-60,61) 
        public IActionResult Bemvindo(string nome, int id)
        {
            // Criando o objetos da página Bem-vindo
            ViewData["Title"] = "Bem Vindos";

            ViewData["Nome"] = nome;
            ViewData["Numerox"] = id;


            return View();
        }
        
    }
}
