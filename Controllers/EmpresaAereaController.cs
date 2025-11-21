using Microsoft.AspNetCore.Mvc;
using VoeMais.Models;
using VoeMais.Repository;

namespace VoeMais.Controllers;

public class EmpresaAereaController : Controller
{
    private readonly IEmpresaAereaRepository _repo;

    public EmpresaAereaController(IEmpresaAereaRepository repo)
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
        return View(new EmpresaAerea());
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmpresaAerea model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _repo.Create(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var empresa = await _repo.GetById(id);
        if (empresa == null) return NotFound();
        return View(empresa);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EmpresaAerea model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _repo.Update(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var empresa = await _repo.GetById(id);
        if (empresa == null) return NotFound();
        return View(empresa);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var empresa = await _repo.GetById(id);
        if (empresa == null) return NotFound();
        return View(empresa);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var empresa = await _repo.GetById(id);
        if (empresa == null) return NotFound();
        await _repo.Delete(empresa);
        return RedirectToAction(nameof(Index));
    }
}
