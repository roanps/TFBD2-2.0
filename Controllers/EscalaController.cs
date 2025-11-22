using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VoeMais.Data;
using VoeMais.Models;
using VoeMais.Repository;
using Microsoft.EntityFrameworkCore;

namespace VoeMais.Controllers
{
    public class EscalaController : Controller
    {
        private readonly IEscalaRepository _repo;
        private readonly IAeroportoRepository _aeroRepo;
        private readonly IVooRepository _vooRepo;
        private readonly IVooEscalaRepository _vooEscalaRepo;

        public EscalaController(
            IEscalaRepository repo,
            IAeroportoRepository aeroRepo,
            IVooRepository vooRepo,
            IVooEscalaRepository vooEscalaRepo)
        {
            _repo = repo;
            _aeroRepo = aeroRepo;
            _vooRepo = vooRepo;
            _vooEscalaRepo = vooEscalaRepo;
        }

        // GET: Escala
        public async Task<IActionResult> Index()
        {
            var escalas = await _repo.GetAll();
            return View(escalas);
        }

        // GET: Escala/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Aeroportos = new SelectList(await _aeroRepo.GetAll(), "IdAeroporto", "Nome");

            // Lista de voos (exibe origem -> destino)
            var voos = await _vooRepo.GetAll();
            ViewBag.Voos = new SelectList(
                voos.Select(v => new
                {
                    v.IdVoo,
                    Descricao = $"{v.IdVoo} - {v.Origem?.CodigoIATA} ? {v.Destino?.CodigoIATA}"
                }),
                "IdVoo",
                "Descricao"
            );

            return View(new Escala());
        }

        // POST: Escala/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Escala escala, int idVoo)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Aeroportos = new SelectList(await _aeroRepo.GetAll(), "IdAeroporto", "Nome");

                var voos = await _vooRepo.GetAll();
                ViewBag.Voos = new SelectList(
                    voos.Select(v => new
                    {
                        v.IdVoo,
                        Descricao = $"{v.IdVoo} - {v.Origem?.CodigoIATA} ? {v.Destino?.CodigoIATA}"
                    }),
                    "IdVoo",
                    "Descricao"
                );

                return View(escala);
            }

            // 1?? Criar escala
            await _repo.Create(escala);

            // 2?? Criar vínculo com o voo automaticamente
            var vooEscala = new VooEscala
            {
                IdVoo = idVoo,
                IdEscala = escala.IdEscala
            };

            await _vooEscalaRepo.Create(vooEscala);

            return RedirectToAction(nameof(Index));
        }

        // GET: Escala/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var escala = await _repo.GetById(id);
            if (escala == null)
                return NotFound();

            ViewBag.Aeroportos = new SelectList(await _aeroRepo.GetAll(), "IdAeroporto", "Nome", escala.IdAeroportoEscala);
            return View(escala);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Escala escala)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Aeroportos = new SelectList(await _aeroRepo.GetAll(), "IdAeroporto", "Nome", escala.IdAeroportoEscala);
                return View(escala);
            }

            await _repo.Update(escala);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var escala = await _repo.GetById(id);
            if (escala == null)
                return NotFound();

            return View(escala);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
