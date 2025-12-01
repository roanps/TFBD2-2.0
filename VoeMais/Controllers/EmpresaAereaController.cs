using Microsoft.AspNetCore.Mvc;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Controllers
{
    public class EmpresaAereaController : Controller
    {
        private readonly IEmpresaAereaRepository _repository;

        public EmpresaAereaController(IEmpresaAereaRepository repository)
        {
            _repository = repository;
        }

        // GET: EmpresaAerea
        public async Task<IActionResult> Index()
        {
            var empresas = await _repository.GetAllAsync();
            return View(empresas);
        }

        // GET: EmpresaAerea/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var empresa = await _repository.GetByIdAsync(id);
            if (empresa == null)
                return NotFound();

            return View(empresa);
        }

        // GET: EmpresaAerea/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpresaAerea/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmpresaAerea empresa)
        {
            if (!ModelState.IsValid)
                return View(empresa);

            await _repository.AddAsync(empresa);
            return RedirectToAction(nameof(Index));
        }

        // GET: EmpresaAerea/Edit/
        public async Task<IActionResult> Edit(int id)
        {
            var empresa = await _repository.GetByIdAsync(id);
            if (empresa == null)
                return NotFound();

            return View(empresa);
        }

        // POST: EmpresaAerea/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmpresaAerea empresa)
        {
            if (id != empresa.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(empresa);

            await _repository.UpdateAsync(empresa);
            return RedirectToAction(nameof(Index));
        }

        // GET: EmpresaAerea/Delete/
        public async Task<IActionResult> Delete(int id)
        {
            var empresa = await _repository.GetByIdAsync(id);
            if (empresa == null)
                return NotFound();

            return View(empresa);
        }

        // POST: EmpresaAerea/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
