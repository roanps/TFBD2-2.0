using Microsoft.AspNetCore.Mvc;
using VoeMais.Models;
using VoeMais.Repository;

namespace VoeMais.Controllers;

public class VooController : Controller
{
    private readonly IVooRepository _repo;

    public VooController(IVooRepository repo)
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
        return View(new Voo());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Voo model)
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
    public async Task<IActionResult> Edit(Voo model)
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
