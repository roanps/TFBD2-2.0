using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Controllers
{
    public class VooController : Controller
    {
        private readonly IVooRepository _vooRepository;
        private readonly IAviaoRepository _aviaoRepository;
        private readonly IAeroportoRepository _aeroportoRepository;
        private readonly IVooPoltronaRepository _vooPoltronaRepository;
        private readonly IPoltronaRepository _poltronaRepository;

        public VooController(
            IVooRepository vooRepository,
            IAviaoRepository aviaoRepository,
            IAeroportoRepository aeroportoRepository,
            IVooPoltronaRepository vooPoltronaRepository,
            IPoltronaRepository poltronaRepository)
        {
            _vooRepository = vooRepository;
            _aviaoRepository = aviaoRepository;
            _aeroportoRepository = aeroportoRepository;
            _vooPoltronaRepository = vooPoltronaRepository;
            _poltronaRepository = poltronaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var voos = await _vooRepository.GetAllAsync();
            return View(voos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var voo = await _vooRepository.GetByIdAsync(id);
            if (voo == null)
                return NotFound();

            return View(voo);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Voo voo)
        {
            if (!ModelState.IsValid)
            {
                await CarregarDropdowns();
                return View(voo);
            }

            await _vooRepository.AddAsync(voo);

            // Gera poltronas para o voo
            await GerarPoltronasDoVoo(voo.Id, voo.AviaoId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var voo = await _vooRepository.GetByIdAsync(id);
            if (voo == null)
                return NotFound();

            await CarregarDropdowns();
            return View(voo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Voo voo)
        {
            if (id != voo.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await CarregarDropdowns();
                return View(voo);
            }

            await _vooRepository.UpdateAsync(voo);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var voo = await _vooRepository.GetByIdAsync(id);
            if (voo == null)
                return NotFound();

            return View(voo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vooRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Dropdown de aviões e aeroportos
        private async Task CarregarDropdowns()
        {
            var avioes = await _aviaoRepository.GetAllAsync();
            var aeroportos = await _aeroportoRepository.GetAllAsync();

            ViewBag.AviaoId = new SelectList(avioes, "Id", "Modelo");
            ViewBag.OrigemId = new SelectList(aeroportos, "Id", "Nome");
            ViewBag.DestinoId = new SelectList(aeroportos, "Id", "Nome");
        }

        // Gera as poltronas do voo a partir das poltronas do avião
        private async Task GerarPoltronasDoVoo(int vooId, int aviaoId)
        {
            var poltronas = await _poltronaRepository.GetAllAsync();
            poltronas = poltronas.Where(p => p.AviaoId == aviaoId).ToList();

            foreach (var poltrona in poltronas)
            {
                var vp = new VooPoltrona
                {
                    VooId = vooId,
                    PoltronaId = poltrona.Id,
                    Status = PoltronaStatus.Livre
                };

                await _vooPoltronaRepository.AddAsync(vp);
            }
        }
    }
}
