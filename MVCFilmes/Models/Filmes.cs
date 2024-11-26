using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCFilmes.Models
{

                    //          Aula 62 Models
                    //          Aula 64 Data Anottations
                    //          Aula 68 Alterações de campo e base de dados 05/05
                    //          Aula 69 Validações
    public class Filmes
    {
        // (1-62)
        public int ID { get; set; }
        // string.Empty -> A variável não fique nula   

        // (5-69)
        [MaxLength(100)]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Titulo { get; set; } = string.Empty;

        // (6-69)
        [Required(ErrorMessage = "Este campo é obrigatório")]
        // (2-64) Alterando a descrição 
        [Display(Name = "Data de Lançamento")]
        [DataType(DataType.Date)]
        public DateTime DataLancamento { get; set; }

        // (7-69)
        [
            Required(ErrorMessage = "Este campo é obrigatório"),
            StringLength(10),
            // Regular impede que o usuário insira um numero
            RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage ="Dados inválidos")
            
        ]
        public string Genero { get; set; } = string.Empty;
        // (3-64) Alterando a descrição Preço e informado ao .NET sobre as casas decimais referente ao preço

        // (8-69)
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }


        // (9-69)
        [Range(0,5, ErrorMessage ="Somente valores de 0 a 5")]
        // (4-68)
        public int Pontos { get; set; }


    }
}
