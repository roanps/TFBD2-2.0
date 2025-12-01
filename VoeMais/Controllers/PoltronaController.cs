using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Controllers
{
    public class PoltronaController : Controller
    {
        private readonly IPoltronaRepository _poltronaRepository;
        private readonly IAviaoRepository _aviaoRepository;

        public PoltronaController(
            IPoltronaRepository poltronaRepository,
            IAviaoRepository aviaoRepository)
        {
            _poltronaRepository = poltronaRepository;
            _aviaoRepository = aviaoRepository;
        }

        // GET: Poltrona
        public async Task<IActionResult> Index()
        {
            var poltronas = await _poltronaRepository.GetAllAsync();
            return View(poltronas);
        }

        // GET: Poltrona/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var poltrona = await _poltronaRepository.GetByIdAsync(id);
            if (poltrona == null)
                return NotFound();

            return View(poltrona);
        }

        // GET: Poltrona/Create
        public async Task<IActionResult> Create()
        {
            await CarregarAvioes();
            return View();
        }

        // POST: Poltrona/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Poltrona poltrona)
        {
            if (!ModelState.IsValid)
            {
                await CarregarAvioes();
                return View(poltrona);
            }

            await _poltronaRepository.AddAsync(poltrona);
            return RedirectToAction(nameof(Index));
        }

        // GET: Poltrona/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var poltrona = await _poltronaRepository.GetByIdAsync(id);
            if (poltrona == null)
                return NotFound();

            await CarregarAvioes();
            return View(poltrona);
        }

        // POST: Poltrona/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Poltrona poltrona)
        {
            if (id != poltrona.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await CarregarAvioes();
                return View(poltrona);
            }

            await _poltronaRepository.UpdateAsync(poltrona);
            return RedirectToAction(nameof(Index));
        }

        // GET: Poltrona/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var poltrona = await _poltronaRepository.GetByIdAsync(id);
            if (poltrona == null)
                return NotFound();

            return View(poltrona);
        }

        // POST: Poltrona/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _poltronaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // MÉTODO AUXILIAR (Dropdown de Aviões)
        private async Task CarregarAvioes()
        {
            var avioes = await _aviaoRepository.GetAllAsync();
            ViewBag.AviaoId = new SelectList(avioes, "Id", "Modelo");
        }
    }
}
