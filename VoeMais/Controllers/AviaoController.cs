using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Controllers
{
    public class AviaoController : Controller
    {
        private readonly IAviaoRepository _aviaoRepository;
        private readonly IEmpresaAereaRepository _empresaRepository;

        public AviaoController(
            IAviaoRepository aviaoRepository,
            IEmpresaAereaRepository empresaRepository)
        {
            _aviaoRepository = aviaoRepository;
            _empresaRepository = empresaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var avioes = await _aviaoRepository.GetAllAsync();
            return View(avioes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var aviao = await _aviaoRepository.GetByIdAsync(id);
            if (aviao == null)
                return NotFound();

            return View(aviao);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarEmpresas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aviao aviao)
        {
            if (!ModelState.IsValid)
            {
                await CarregarEmpresas();
                return View(aviao);
            }

            await _aviaoRepository.AddAsync(aviao);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var aviao = await _aviaoRepository.GetByIdAsync(id);
            if (aviao == null)
                return NotFound();

            await CarregarEmpresas();
            return View(aviao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aviao aviao)
        {
            if (id != aviao.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await CarregarEmpresas();
                return View(aviao);
            }

            await _aviaoRepository.UpdateAsync(aviao);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var aviao = await _aviaoRepository.GetByIdAsync(id);
            if (aviao == null)
                return NotFound();

            return View(aviao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _aviaoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarEmpresas()
        {
            var empresas = await _empresaRepository.GetAllAsync();
            ViewBag.EmpresaAereaId = new SelectList(empresas, "Id", "Nome");
        }
    }
}
