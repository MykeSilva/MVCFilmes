using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCFilmes.Models
{

    /*                      Aula 67 View Model 04/05/24     */
    public class FilmesViewModel
    {
        /*(1-67)*/
        public List<Filmes> ? Filmes { get; set; }
        public SelectList ? Generos {  get; set; }
        public string ? Genero {  get; set; }
        public string ? Texto { get; set; }

    }
}
