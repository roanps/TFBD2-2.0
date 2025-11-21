using Microsoft.AspNetCore.Mvc;
using VoeMais.Models;
using VoeMais.Repository;

namespace VoeMais.Controllers;

public class EscalaController : Controller
{
    private readonly IEscalaRepository _repo;

    public EscalaController(IEscalaRepository repo)
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
        return View(new Escala());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Escala model)
    {
        if (!ModelState.IsValid) return View(model);
        await _repo.Create(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _repo.GetById(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Escala model)
    {
        if (!ModelState.IsValid) return View(model);
        await _repo.Update(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _repo.GetById(id);
        if (item == null) return NotFound();
        return View(item);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _repo.GetById(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _repo.GetById(id);
        if (item == null) return NotFound();
        await _repo.Delete(item);
        return RedirectToAction(nameof(Index));
    }
}
