using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
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
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context; //instância do contexto
            _mapper = mapper;
        }

        //[FromBody] -> especifica que parametro do Post vem através do corpo da requisição
        //Inserção de recursos no sistema
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            //retorna o objeto criado para o usuário e informa qual caminho é possivel encontrar
            return CreatedAtAction(nameof(RecuperaFilmePorId),
                new { id = filme.Id }, filme);
        }

        //Leitura de todos os filmes do sistema
        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes(
            [FromQuery] int skip = 0, [FromQuery] int take = 50)
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

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = _context.Filmes.FirstOrDefault(
                filme => filme.Id == id);
            if (filme == null) return NotFound();
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
