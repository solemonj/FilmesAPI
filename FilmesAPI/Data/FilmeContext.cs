using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    //essa classe é um contexto de banco de dados
    //deve ser criada apos baixar e instalar entity
    //deve extender de Db Context
    public class FilmeContext : DbContext
    {
        //define o construtor da classe, que recebe as configurações de acesso ao banco
        //faz a passagem para o construtor "base" = dbcontext
        public FilmeContext(DbContextOptions<FilmeContext> opts) : base(opts)
        {
            
        }

        //cria uma propriedade de acesso para dar acesso aos filmes que teremos na base
        //DbSet = conjunto de dados
        public DbSet<Filme> Filmes { get; set; }

    }
}
