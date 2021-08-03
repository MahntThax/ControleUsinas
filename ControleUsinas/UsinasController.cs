using ControleUsinas.Data;
using ControleUsinas.Models;
using ControleUsinas.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleUsinas
{
    public class UsinasController : Controller
    {
        private readonly DbContexto _context;

        public UsinasController(DbContexto context) => this._context = context;

        // GET: Usinas
        public IActionResult Index(
            string ordenador,
            string filtroFornecedor,
            string filtroFornecedorAtual,
            string filtroUC,
            string filtroUCAtual,
            string filtroAtivo,
            string filtroAtivoAtual,
            int paginaAtual,
            int tamanhoPorPagina)
        {
            this.ViewData["FornecedorOrdenador"] = string.IsNullOrEmpty(ordenador) ? "fornecedor_desc" : string.Empty;
            this.ViewData["UCOrdenador"] = ordenador == "UC" ? "UC_desc" : "UC";
            this.ViewData["OrdenadorAtual"] = ordenador;
            this.ViewData["FornecedorFiltroAtual"] = filtroFornecedor;
            this.ViewData["UCFiltroAtual"] = filtroUC;
            this.ViewData["AtivoFiltroAtual"] = filtroAtivo;

            var listaFiltroAtivo = new List<SelectListItem>
            {
                new SelectListItem { Value = "todos", Text = "Todos", Selected = false },
                new SelectListItem { Value = "sim", Text = "Sim", Selected = false },
                new SelectListItem { Value = "nao", Text = "Não", Selected = false },
            };
            var listaTamanhosPaginas = new List<SelectListItem>
            {
                new SelectListItem { Value = "10", Text = "10", Selected = false },
                new SelectListItem { Value = "20", Text = "20", Selected = false },
                new SelectListItem { Value = "30", Text = "30", Selected = false },
            };

            if (tamanhoPorPagina == 0)
            {
                tamanhoPorPagina = 10;
            }
            if (paginaAtual == 0)
            {
                paginaAtual = 1;
            }

            listaTamanhosPaginas.First(i => i.Value.Equals(tamanhoPorPagina.ToString())).Selected = true;
            this.ViewData["ListaTamanhosPaginas"] = listaTamanhosPaginas;

            var usinas = this._context.Usinas.AsQueryable();

            if (!(filtroFornecedor is null) || !(filtroUC is null) || !(filtroAtivo is null))
            {
                paginaAtual = 1;
            }
            else
            {
                filtroAtivo = filtroAtivoAtual;
                filtroFornecedor = filtroFornecedorAtual;
                filtroUC = filtroUCAtual;
            }

            if (!string.IsNullOrEmpty(filtroFornecedor))
            {
                usinas = usinas.Where(
                    u => u.Fornecedor.Contains(filtroFornecedor));
            }

            if (!string.IsNullOrEmpty(filtroUC))
            {
                usinas = usinas.Where(
                    u => u.Fornecedor.Contains(filtroUC));
            }

            if (!string.IsNullOrEmpty(filtroAtivo))
            {
                listaFiltroAtivo.First(i => i.Value.Equals(filtroAtivo)).Selected = true;

                switch (filtroAtivo)
                {
                    case "sim":
                        usinas = usinas.Where(u => u.Ativo);
                        break;
                    case "nao":
                        usinas = usinas.Where(u => !u.Ativo);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                listaFiltroAtivo[0].Selected = true;
            }
            this.ViewData["ListaFiltroAtivo"] = listaFiltroAtivo.ToList();

            switch (ordenador)
            {
                case "fornecedor_desc":
                    usinas = usinas.OrderByDescending(u => u.Fornecedor);
                    break;
                case "UC_desc":
                    usinas = usinas.OrderByDescending(u => u.UC);
                    break;
                case "UC":
                    usinas = usinas.OrderBy(u => u.UC);
                    break;
                default:
                    usinas = usinas.OrderBy(u => u.Fornecedor);
                    break;
            }

            return this.View(PagingHelper<Usina>.CriaPaginacao(usinas, paginaAtual, tamanhoPorPagina));
        }

        // GET: Usinas/Create
        public IActionResult Create() => this.View();

        // POST: Usinas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ativo,Fornecedor,UC")] Usina usina)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    this._context.Add(usina);
                    await this._context.SaveChangesAsync();
                    return this.RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    this.ModelState.AddModelError(string.Empty, "Não foi possível criar o cadastro, por favor tente novamente." +
                        "Se o erro persistir, entre em contato com a administração.");
                }
            }
            return this.View(usina);
        }

        // GET: Usinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var usina = await this._context.Usinas.FindAsync(id);
            if (usina == null)
            {
                return this.NotFound();
            }
            return this.View(usina);
        }

        // POST: Usinas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ativo,Fornecedor,UC")] Usina usina)
        {
            if (id != usina.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this._context.Update(usina);
                    await this._context.SaveChangesAsync();
                    return this.RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.UsinaExists(usina.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, "Não foi possível salvar as alterações, por favor tente novamente." +
                            "Se o error persistir, entre em contato com a administração");
                    }
                }
            }
            return this.View(usina);
        }

        // GET: Usinas/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? erroNoSaveChanges = false)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var usina = await this._context.Usinas
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (usina == null)
            {
                return this.NotFound();
            }

            if (erroNoSaveChanges.GetValueOrDefault())
            {
                this.ViewData["MensagemErro"] =
                    "Não foi possível apagar, por favor tente novamente." +
                    "Se o erro persistir, entre em contato com a administração";
            }

            return this.View(usina);
        }

        // POST: Usinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usina = await this._context.Usinas.FindAsync(id);
            
            if (usina is null)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            try
            {
                this._context.Usinas.Remove(usina);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return this.RedirectToAction(nameof(this.Delete), new { id, erroNoSaveChanges = true });
            }

        }

        private bool UsinaExists(int id) => this._context.Usinas.Any(e => e.Id == id);
    }
}
