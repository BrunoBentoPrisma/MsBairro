using Microsoft.EntityFrameworkCore;
using MsBairro.Repositorys.Entidades;

namespace MsBairro.MsContext
{
    public class MsBairroContext : DbContext
    {
        public MsBairroContext(DbContextOptions<MsBairroContext> options)
            : base(options)
        {
        }

        public DbSet<Bairro> Bairro { get; set; }
    }
}
