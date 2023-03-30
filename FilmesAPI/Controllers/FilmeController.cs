using FilmesAPI.Data;
using FilmesAPI.Models;//
using Microsoft.AspNetCore.Mvc;//
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        //injeção de dependencia
        private FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context; //instância do contexto
        }

        //[FromBody] -> especifica que parametro do Post vem através do corpo da requisição
        //Inserção de recursos no sistema
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            //retorna o objeto criado para o usuário e informa qual caminho é possivel encontrar
            return CreatedAtAction(nameof(RecuperaFilmePorId), 
                new {id = filme.Id}, filme);
        }

        //Leitura de todos os filmes do sistema
        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes(
            [FromQuery] int skip = 0,[FromQuery] int take = 50)
        {
            return _context.Filmes.Skip(skip).Take(take);
        }

        //recuperacao de um filme unico atraves do id passado no getF
        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            return Ok(filme);
        
        }
    }
}
