using Microsoft.AspNetCore.Mvc;
using VoeMais.Models;
using VoeMais.Repository;

namespace VoeMais.Controllers;

public class PassagemController : Controller
{
    private readonly IPassagemRepository _repo;

    public PassagemController(IPassagemRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _repo.GetAll();
        return View(list);
    }

    public IActionResult Create()
    {
        return View(new Passagem());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Passagem model)
    {
        if (!ModelState.IsValid) return View(model);
        await _repo.Create(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int idPassageiro, int idVoo)
    {
        var item = await _repo.GetById(idPassageiro, idVoo);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Passagem model)
    {
        if (!ModelState.IsValid) return View(model);
        await _repo.Update(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int idPassageiro, int idVoo)
    {
        var item = await _repo.GetById(idPassageiro, idVoo);
        if (item == null) return NotFound();
        return View(item);
    }

    public async Task<IActionResult> Delete(int idPassageiro, int idVoo)
    {
        var item = await _repo.GetById(idPassageiro, idVoo);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int idPassageiro, int idVoo)
    {
        var item = await _repo.GetById(idPassageiro, idVoo);
        if (item == null) return NotFound();
        await _repo.Delete(item);
        return RedirectToAction(nameof(Index));
    }
}
