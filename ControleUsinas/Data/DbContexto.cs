using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleUsinas.Models;

namespace ControleUsinas.Data
{
    public class DbContexto : DbContext
    {
        public DbContexto(DbContextOptions<DbContexto> opcoes) : base(opcoes) { }

        public DbSet<Usina> Usinas { get; set; }
    }
}
