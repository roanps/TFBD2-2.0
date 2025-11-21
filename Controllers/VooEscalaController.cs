using Microsoft.AspNetCore.Mvc;
using VoeMais.Models;
using VoeMais.Repository;

namespace VoeMais.Controllers;

public class VooEscalaController : Controller
{
    private readonly IVooEscalaRepository _repo;

    public VooEscalaController(IVooEscalaRepository repo)
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
        return View(new VooEscala());
    }

    [HttpPost]
    public async Task<IActionResult> Create(VooEscala model)
    {
        if (!ModelState.IsValid) return View(model);
        await _repo.Create(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int idVoo, int idEscala)
    {
        var item = await _repo.GetById(idVoo, idEscala);
        if (item == null) return NotFound();
        return View(item);
    }

    public async Task<IActionResult> Delete(int idVoo, int idEscala)
    {
        var item = await _repo.GetById(idVoo, idEscala);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int idVoo, int idEscala)
    {
        var item = await _repo.GetById(idVoo, idEscala);
        if (item == null) return NotFound();
        await _repo.Delete(item);
        return RedirectToAction(nameof(Index));
    }
}
