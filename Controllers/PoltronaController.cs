using Microsoft.AspNetCore.Mvc;
using VoeMais.Models;
using VoeMais.Repository;

namespace VoeMais.Controllers;

public class PoltronaController : Controller
{
    private readonly IPoltronaRepository _repo;

    public PoltronaController(IPoltronaRepository repo)
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
        return View(new Poltrona());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Poltrona model)
    {
        if (!ModelState.IsValid) return View(model);
        await _repo.Create(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int idVoo, string numeroPoltrona)
    {
        var item = await _repo.GetById(idVoo, numeroPoltrona);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Poltrona model)
    {
        if (!ModelState.IsValid) return View(model);
        await _repo.Update(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int idVoo, string numeroPoltrona)
    {
        var item = await _repo.GetById(idVoo, numeroPoltrona);
        if (item == null) return NotFound();
        return View(item);
    }

    public async Task<IActionResult> Delete(int idVoo, string numeroPoltrona)
    {
        var item = await _repo.GetById(idVoo, numeroPoltrona);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int idVoo, string numeroPoltrona)
    {
        var item = await _repo.GetById(idVoo, numeroPoltrona);
        if (item == null) return NotFound();
        await _repo.Delete(item);
        return RedirectToAction(nameof(Index));
    }
}
