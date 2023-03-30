using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new();
        private static int id =0;


        //[FromBody] -> especifica que parametro do Post vem através do corpo da requisição
        //Inserção de recursos no sistema
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            filme.Id = id++;
            filmes.Add(filme);
            //retorna o objeto criado para o usuário e informa qual caminho é possivel encontrar
            return CreatedAtAction(nameof(RecuperaFilmePorId), 
                new {id = filme.Id}, filme);
        }

        //Leitura de todos os filmes do sistema
        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes(
            [FromQuery] int skip = 0,[FromQuery] int take = 50)
        {
            return filmes.Skip(skip).Take(take);
        }

        //recuperacao de um filme unico atraves do id passado no getF
        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {
            var filme = filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            return Ok(filme);
        
        }
    }
}
