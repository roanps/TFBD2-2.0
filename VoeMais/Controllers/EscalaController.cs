using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Controllers
{
    public class EscalaController : Controller
    {
        private readonly IEscalaRepository _escalaRepository;
        private readonly IVooRepository _vooRepository;
        private readonly IAeroportoRepository _aeroportoRepository;

        public EscalaController(
            IEscalaRepository escalaRepository,
            IVooRepository vooRepository,
            IAeroportoRepository aeroportoRepository)
        {
            _escalaRepository = escalaRepository;
            _vooRepository = vooRepository;
            _aeroportoRepository = aeroportoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var escalas = await _escalaRepository.GetAllAsync();
            return View(escalas);
        }

        public async Task<IActionResult> Details(int id)
        {
            var escala = await _escalaRepository.GetByIdAsync(id);
            if (escala == null)
                return NotFound();

            return View(escala);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Escala escala)
        {
            if (!ModelState.IsValid)
            {
                await CarregarDropdowns();
                return View(escala);
            }

            await _escalaRepository.AddAsync(escala);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var escala = await _escalaRepository.GetByIdAsync(id);
            if (escala == null)
                return NotFound();

            await CarregarDropdowns();
            return View(escala);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Escala escala)
        {
            if (id != escala.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await CarregarDropdowns();
                return View(escala);
            }

            await _escalaRepository.UpdateAsync(escala);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var escala = await _escalaRepository.GetByIdAsync(id);
            if (escala == null)
                return NotFound();

            return View(escala);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _escalaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Dropdowns de voos e aeroportos
        private async Task CarregarDropdowns()
        {
            var voos = await _vooRepository.GetAllAsync();
            var aeroportos = await _aeroportoRepository.GetAllAsync();

            ViewBag.VooId = new SelectList(voos, "Id", "Codigo");
            ViewBag.AeroportoId = new SelectList(aeroportos, "Id", "Nome");
        }
    }
}
