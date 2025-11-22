using Microsoft.AspNetCore.Mvc;
using VoeMais.Models;
using VoeMais.Repository;

namespace VoeMais.Controllers
{
    public class VooController : Controller
    {
        private readonly IVooRepository _repo;
        private readonly IAeroportoRepository _aeroRepo;
        private readonly IAeronaveRepository _aeronaveRepo;
        private readonly IVooEscalaRepository _vooEscalaRepo;
        private readonly IEscalaRepository _escalaRepo;

        public VooController(
            IVooRepository repo,
            IAeroportoRepository aeroRepo,
            IAeronaveRepository aeronaveRepo,
            IVooEscalaRepository vooEscalaRepo,
            IEscalaRepository escalaRepo)
        {
            _repo = repo;
            _aeroRepo = aeroRepo;
            _aeronaveRepo = aeronaveRepo;
            _vooEscalaRepo = vooEscalaRepo;
            _escalaRepo = escalaRepo;
        }

        public async Task<IActionResult> Index()
        {
            var voos = await _repo.GetAll();
            return View(voos);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Aeronaves = await _aeronaveRepo.GetAll();
            ViewBag.Aeroportos = await _aeroRepo.GetAll();
            return View(new Voo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Voo model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Aeronaves = await _aeronaveRepo.GetAll();
                ViewBag.Aeroportos = await _aeroRepo.GetAll();
                return View(model);
            }

            await _repo.Create(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var voo = await _repo.GetById(id);
            if (voo == null)
                return NotFound();

            return View(voo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var voo = await _repo.GetById(id);
            if (voo == null)
                return NotFound();

            ViewBag.Aeronaves = await _aeronaveRepo.GetAll();
            ViewBag.Aeroportos = await _aeroRepo.GetAll();
            return View(voo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Voo model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Aeronaves = await _aeronaveRepo.GetAll();
                ViewBag.Aeroportos = await _aeroRepo.GetAll();
                return View(model);
            }

            await _repo.Update(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var voo = await _repo.GetByIdWithEscalas(id);
            if (voo == null)
                return NotFound();

            if (voo.VoosEscalas.Any())
                ViewBag.Alerta = $"Este voo possui {voo.VoosEscalas.Count} escala(s) vinculada(s). Ao excluir, as escalas também serão removidas.";

            return View(voo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voo = await _repo.GetByIdWithEscalas(id);
            if (voo == null)
                return NotFound();

            foreach (var ve in voo.VoosEscalas)
                await _escalaRepo.Delete(ve.IdEscala);

            await _vooEscalaRepo.DeleteByVoo(id);
            await _repo.Delete(voo);

            return RedirectToAction(nameof(Index));
        }
    }
}
