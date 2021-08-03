using ControleUsinas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleUsinas.Data
{
    public static class InicializadorBD
    {
        public static void Inicializa(DbContexto contexto)
        {
            contexto.Database.EnsureCreated();

            if (!contexto.Usinas.Any())
            {
                var usinas = new Usina[]
                {
                    new Usina
                    {
                        Fornecedor = "Solarian",
                        UC = "42953009",
                    },
                    new Usina
                    {
                        Fornecedor = "Futura",
                        UC = "42953010",
                    },
                    new Usina
                    {
                        Fornecedor = "Central Geradora Fazenda Modelo",
                        UC = "42953011",
                    },
                    new Usina
                    {
                        Fornecedor = "Nova Mundo",
                        UC = "42953012",
                    },
                    new Usina
                    {
                        Fornecedor = "Solare",
                        UC = "42953013",
                    },
                    new Usina
                    {
                        Fornecedor = "Unisol",
                        UC = "42953014",
                    },
                };

                contexto.Usinas.AddRange(usinas);

                contexto.SaveChanges();
            }
        }
    }
}
