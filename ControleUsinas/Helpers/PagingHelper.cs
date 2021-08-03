using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ControleUsinas.Helpers
{
    public class PagingHelper<T> : List<T>
    {
        public int PaginaAtual { get; private set; }
        public int PaginasTotais { get; private set; }

        public PagingHelper(List<T> items, int quantidadePaginas, int oaginaAtual, int quantidadePorPagina)
        {
            this.PaginaAtual = oaginaAtual;
            this.PaginasTotais = (int)Math.Ceiling(quantidadePaginas / (double)quantidadePorPagina);

            this.AddRange(items);
        }

        public bool TemPaginaAnterior => this.PaginaAtual > 1;

        public bool TemProximaPagina => this.PaginaAtual < this.PaginasTotais;

        public static PagingHelper<T> CriaPaginacao(IQueryable<T> source, int paginaAtual, int tamanhoPorPagina)
        {
            var count = source.Count();
            var items = source.Skip((paginaAtual - 1) * tamanhoPorPagina).Take(tamanhoPorPagina).ToList();
            return new PagingHelper<T>(items, count, paginaAtual, tamanhoPorPagina);
        }
    }
}
