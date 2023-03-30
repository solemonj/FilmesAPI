using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required (ErrorMessage ="O filme deve possuir um nome")]
        [MaxLength(60,ErrorMessage ="O nome do filme está muito grande")]
        [MinLength(3, ErrorMessage = "O nome do filme não deve ser tão pequeno")]
        public string Titulo { get; set; }


        [Required(ErrorMessage = "O filme deve possuir um gênero")]
        [MaxLength(60, ErrorMessage = "O gênero do filme está muito grande")]
        [MinLength(3, ErrorMessage = "O gênero do filme não deve ser tão pequeno")]
        public string Genero { get; set; }


        [Required(ErrorMessage = "O filme deve possuir uma duração")]
        [Range(50, 600, ErrorMessage = "A duração do filme deve estar entre 50 e 600 minutoa")]
        public int Duracao { get; set; }
    }
}