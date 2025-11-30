using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VoeMais.Data;
using VoeMais.Models;

namespace VoeMais.Controllers
{
    public class AviaoController : Controller
    {
        private readonly VoeMaisContext _context;

        public AviaoController(VoeMaisContext context)
        {
            _context = context;
        }

        // GET: Aviao
        public async Task<IActionResult> Index()
        {
            var voeMaisContext = _context.Avioes.Include(a => a.EmpresaAerea);
            return View(await voeMaisContext.ToListAsync());
        }

        // GET: Aviao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aviao = await _context.Avioes
                .Include(a => a.EmpresaAerea)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aviao == null)
            {
                return NotFound();
            }

            return View(aviao);
        }

        // GET: Aviao/Create
        public IActionResult Create()
        {
            ViewData["EmpresaAereaId"] = new SelectList(_context.EmpresasAereas, "Id", "Id");
            return View();
        }

        // POST: Aviao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Modelo,Prefixo,Capacidade,EmpresaAereaId")] Aviao aviao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aviao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaAereaId"] = new SelectList(_context.EmpresasAereas, "Id", "Id", aviao.EmpresaAereaId);
            return View(aviao);
        }

        // GET: Aviao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aviao = await _context.Avioes.FindAsync(id);
            if (aviao == null)
            {
                return NotFound();
            }
            ViewData["EmpresaAereaId"] = new SelectList(_context.EmpresasAereas, "Id", "Id", aviao.EmpresaAereaId);
            return View(aviao);
        }

        // POST: Aviao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Modelo,Prefixo,Capacidade,EmpresaAereaId")] Aviao aviao)
        {
            if (id != aviao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aviao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AviaoExists(aviao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaAereaId"] = new SelectList(_context.EmpresasAereas, "Id", "Id", aviao.EmpresaAereaId);
            return View(aviao);
        }

        // GET: Aviao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aviao = await _context.Avioes
                .Include(a => a.EmpresaAerea)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aviao == null)
            {
                return NotFound();
            }

            return View(aviao);
        }

        // POST: Aviao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aviao = await _context.Avioes.FindAsync(id);
            if (aviao != null)
            {
                _context.Avioes.Remove(aviao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AviaoExists(int id)
        {
            return _context.Avioes.Any(e => e.Id == id);
        }
    }
}
