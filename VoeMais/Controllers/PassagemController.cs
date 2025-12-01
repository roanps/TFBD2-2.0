using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VoeMais.Models;
using VoeMais.Repositories.Interfaces;

namespace VoeMais.Controllers
{
    public class PassagemController : Controller
    {
        private readonly IPassagemRepository _passagemRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IVooPoltronaRepository _vooPoltronaRepository;
        private readonly IVooRepository _vooRepository;

        public PassagemController(
            IPassagemRepository passagemRepository,
            IClienteRepository clienteRepository,
            IVooPoltronaRepository vooPoltronaRepository,
            IVooRepository vooRepository)
        {
            _passagemRepository = passagemRepository;
            _clienteRepository = clienteRepository;
            _vooPoltronaRepository = vooPoltronaRepository;
            _vooRepository = vooRepository;
        }

        public async Task<IActionResult> Index()
        {
            var passagens = await _passagemRepository.GetAllAsync();
            return View(passagens);
        }

        public async Task<IActionResult> Details(int id)
        {
            var passagem = await _passagemRepository.GetByIdAsync(id);
            if (passagem == null)
                return NotFound();

            return View(passagem);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Passagem passagem)
        {
            if (!ModelState.IsValid)
            {
                await CarregarDropdowns();
                return View(passagem);
            }

            passagem.DataCompra = DateTime.Now;

            await _passagemRepository.AddAsync(passagem);

            // marca poltrona como ocupada
            var voopol = await _vooPoltronaRepository.GetByIdAsync(passagem.VooPoltronaId);
            if (voopol != null)
            {
                voopol.Status = PoltronaStatus.Ocupada;
                await _vooPoltronaRepository.UpdateAsync(voopol);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var passagem = await _passagemRepository.GetByIdAsync(id);
            if (passagem == null)
                return NotFound();

            await CarregarDropdowns();
            return View(passagem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Passagem passagem)
        {
            if (id != passagem.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await CarregarDropdowns();
                return View(passagem);
            }

            await _passagemRepository.UpdateAsync(passagem);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var passagem = await _passagemRepository.GetByIdAsync(id);
            if (passagem == null)
                return NotFound();

            return View(passagem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passagem = await _passagemRepository.GetByIdAsync(id);

            // libera poltrona de voo
            if (passagem != null)
            {
                var voopol = await _vooPoltronaRepository.GetByIdAsync(passagem.VooPoltronaId);
                if (voopol != null)
                {
                    voopol.Status = PoltronaStatus.Livre;
                    await _vooPoltronaRepository.UpdateAsync(voopol);
                }

                await _passagemRepository.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        // Dropdowns de cliente e poltronas de voo
        private async Task CarregarDropdowns()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            var voopol = await _vooPoltronaRepository.GetAllAsync();
            var voos = await _vooRepository.GetAllAsync();

            ViewBag.ClienteId = new SelectList(clientes, "Id", "Nome");

            // Exibe poltrona + voo (Ex: A1 - Voo 1234)
            ViewBag.VooPoltronaId = new SelectList(
                voopol.Select(v => new {
                    v.Id,
                    Display = $"{v.Poltrona.Numero} - {v.Voo.Codigo}"
                }),
                "Id",
                "Display"
            );
        }
    }
}
