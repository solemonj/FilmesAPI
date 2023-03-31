using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class CreateFilmeDto
    {
        [Required(ErrorMessage = "O filme deve possuir um nome")]
        [StringLength(60, ErrorMessage = "O nome do filme está muito grande")]
        public string Titulo { get; set; }


        [Required(ErrorMessage = "O filme deve possuir um gênero")]
        [StringLength(60, ErrorMessage = "O gênero do filme está muito grande")]
        public string Genero { get; set; }


        [Required(ErrorMessage = "O filme deve possuir uma duração")]
        [Range(50, 600, ErrorMessage = "A duração do filme deve estar entre 50 e 600 minutoa")]
        public int Duracao { get; set; }
    }
}
