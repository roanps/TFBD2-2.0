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

        public VooController(
            IVooRepository vooRepository,
            IAviaoRepository aviaoRepository,
            IAeroportoRepository aeroportoRepository,
            IVooPoltronaRepository vooPoltronaRepository
        )
        {
            _vooRepository = vooRepository;
            _aviaoRepository = aviaoRepository;
            _aeroportoRepository = aeroportoRepository;
            _vooPoltronaRepository = vooPoltronaRepository;
        }

        // GET: Voo
        public async Task<IActionResult> Index()
        {
            var voos = await _vooRepository.GetAllAsync();
            return View(voos);
        }

        // GET: Voo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var voo = await _vooRepository.GetByIdAsync(id.Value);
            if (voo == null) return NotFound();

            return View(voo);
        }

        // GET: Voo/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.AviaoId = new SelectList(await _aviaoRepository.GetAllAsync(), "Id", "Nome");
            ViewBag.OrigemId = new SelectList(await _aeroportoRepository.GetAllAsync(), "Id", "Nome");
            ViewBag.DestinoId = new SelectList(await _aeroportoRepository.GetAllAsync(), "Id", "Nome");

            return View();
        }

        // POST: Voo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Voo voo)
        {
            if (!ModelState.IsValid)
            {
                return View(voo);
            }

            // 1. Criar o voo
            await _vooRepository.AddAsync(voo);

            // 2. Buscar avião
            var aviao = await _aviaoRepository.GetByIdAsync(voo.AviaoId);

            // 3. Gerar poltronas do voo
            if (aviao?.Poltronas != null)
            {
                foreach (var poltrona in aviao.Poltronas)
                {
                    var vooPoltrona = new VooPoltrona
                    {
                        VooId = voo.Id,
                        PoltronaId = poltrona.Id,
                        Status = PoltronaStatus.Livre//Utilizando enum
                    };

                    await _vooPoltronaRepository.AddAsync(vooPoltrona);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Voo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var voo = await _vooRepository.GetByIdAsync(id.Value);
            if (voo == null) return NotFound();

            ViewBag.AviaoId = new SelectList(await _aviaoRepository.GetAllAsync(), "Id", "Nome", voo.AviaoId);
            ViewBag.OrigemId = new SelectList(await _aeroportoRepository.GetAllAsync(), "Id", "Nome", voo.OrigemId);
            ViewBag.DestinoId = new SelectList(await _aeroportoRepository.GetAllAsync(), "Id", "Nome", voo.DestinoId);

            return View(voo);
        }

        // POST: Voo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Voo voo)
        {
            if (id != voo.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(voo);
            }

            await _vooRepository.UpdateAsync(voo);
            return RedirectToAction(nameof(Index));
        }

        // GET: Voo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var voo = await _vooRepository.GetByIdAsync(id.Value);
            if (voo == null) return NotFound();

            return View(voo);
        }

        // POST: Voo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vooRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
