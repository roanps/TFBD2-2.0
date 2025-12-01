using Microsoft.AspNetCore.Mvc;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Controllers
{
    public class AeroportoController : Controller
    {
        private readonly IAeroportoRepository _repository;

        public AeroportoController(IAeroportoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var aeroporto = await _repository.GetByIdAsync(id);
            if (aeroporto == null)
                return NotFound();

            return View(aeroporto);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aeroporto aeroporto)
        {
            if (!ModelState.IsValid)
                return View(aeroporto);

            await _repository.AddAsync(aeroporto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var aeroporto = await _repository.GetByIdAsync(id);
            if (aeroporto == null)
                return NotFound();

            return View(aeroporto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aeroporto aeroporto)
        {
            if (id != aeroporto.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(aeroporto);

            await _repository.UpdateAsync(aeroporto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var aeroporto = await _repository.GetByIdAsync(id);
            if (aeroporto == null)
                return NotFound();

            return View(aeroporto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
