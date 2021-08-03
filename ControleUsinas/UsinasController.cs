using ControleUsinas.Data;
using ControleUsinas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ControleUsinas
{
    public class UsinasController : Controller
    {
        private readonly DbContexto _context;

        public UsinasController(DbContexto context) => this._context = context;

        // GET: Usinas
        public async Task<IActionResult> Index() => this.View(await this._context.Usinas.ToListAsync());

        // GET: Usinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var usina = await this._context.Usinas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usina == null)
            {
                return this.NotFound();
            }

            return this.View(usina);
        }

        // GET: Usinas/Create
        public IActionResult Create() => this.View();

        // POST: Usinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
