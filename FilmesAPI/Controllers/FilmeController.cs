using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;//
using Microsoft.AspNetCore.JsonPatch;
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
        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
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
        public IEnumerable<ReadFilmeDto> RecuperaFilmes(
            [FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
        }

        //recuperacao de um filme unico atraves do id passado no getF
        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound();
            var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            return Ok(filmeDto);
        }

        //Faz alterações de filmes passando todo o objeto
        //UpdateFilmeDTO
        //FilmeProfile
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

        //Faz alterações parciais de filmes
        //lib- newtonsoft
        //UpdateFilmeDTO
        //FilmeProfile
        [HttpPatch("{id}")]
        public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
        {
            var filme = _context.Filmes.FirstOrDefault(
                filme => filme.Id == id);
            if (filme == null) return NotFound();
            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
            patch.ApplyTo(filmeParaAtualizar, ModelState);
            if (!TryValidateModel(filmeParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();
            return NoContent();
        }

        //Deleção
        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(
                filme => filme.Id == id);
            if (filme == null) return NotFound();
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
