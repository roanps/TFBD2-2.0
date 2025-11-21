using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoeMais.Models;
using VoeMais.Repository;

namespace VoeMais.Controllers
{
    public class VooController : Controller
    {
        private readonly IVooRepository _repo;
        private readonly IVooEscalaRepository _vooEscalaRepo;
        private readonly IEscalaRepository _escalaRepo;

        public VooController(
            IVooRepository repo,
            IVooEscalaRepository vooEscalaRepo,
            IEscalaRepository escalaRepo)
        {
            _repo = repo;
            _vooEscalaRepo = vooEscalaRepo;
            _escalaRepo = escalaRepo;
        }

        // =========================
        // LISTAGEM
        // =========================
        public async Task<IActionResult> Index()
        {
            var list = await _repo.GetAll();
            return View(list);
        }

        // =========================
        // CREATE
        // =========================
        public IActionResult Create()
        {
            return View(new Voo());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Voo model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _repo.Create(model);
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // EDITAR
        // =========================
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _repo.GetById(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Voo model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _repo.Update(model);
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DETALHES
        // =========================
        public async Task<IActionResult> Details(int id)
        {
            var item = await _repo.GetById(id);
            if (item == null) return NotFound();

            return View(item);
        }

        // =========================
        // DELETE (GET) — AVISO
        // =========================
        public async Task<IActionResult> Delete(int id)
        {
            var voo = await _repo.GetByIdWithEscalas(id);

            if (voo == null)
                return NotFound();

            if (voo.VoosEscalas.Any())
            {
                ViewBag.Alerta =
                    $"? Este voo possui {voo.VoosEscalas.Count} escala(s) vinculada(s)." +
                    " Ao excluir o voo, TODAS serão excluídas também.";
            }

            return View(voo);
        }

        // =========================
        // DELETE (POST) — EXCLUSÃO TOTAL
        // =========================
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voo = await _repo.GetByIdWithEscalas(id);

            if (voo == null)
                return NotFound();

            // 1?? Excluir escalas vinculadas
            foreach (var ve in voo.VoosEscalas)
            {
                await _escalaRepo.Delete(ve.IdEscala);
            }

            // 2?? Excluir vínculos
            await _vooEscalaRepo.DeleteByVoo(id);

            // 3?? Excluir o voo
            await _repo.Delete(voo);

            return RedirectToAction(nameof(Index));
        }
    }
}
