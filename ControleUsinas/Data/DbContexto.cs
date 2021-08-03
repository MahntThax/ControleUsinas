using ControleUsinas.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleUsinas.Data
{
    public class DbContexto : DbContext
    {
        public DbContexto(DbContextOptions<DbContexto> opcoes) : base(opcoes) { }

        public DbSet<Usina> Usinas { get; set; }
    }
}
