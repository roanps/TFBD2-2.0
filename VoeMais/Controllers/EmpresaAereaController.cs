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
    public class EmpresaAereaController : Controller
    {
        private readonly VoeMaisContext _context;

        public EmpresaAereaController(VoeMaisContext context)
        {
            _context = context;
        }

        // GET: EmpresaAerea
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmpresasAereas.ToListAsync());
        }

        // GET: EmpresaAerea/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaAerea = await _context.EmpresasAereas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresaAerea == null)
            {
                return NotFound();
            }

            return View(empresaAerea);
        }

        // GET: EmpresaAerea/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpresaAerea/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] EmpresaAerea empresaAerea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresaAerea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empresaAerea);
        }

        // GET: EmpresaAerea/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaAerea = await _context.EmpresasAereas.FindAsync(id);
            if (empresaAerea == null)
            {
                return NotFound();
            }
            return View(empresaAerea);
        }

        // POST: EmpresaAerea/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] EmpresaAerea empresaAerea)
        {
            if (id != empresaAerea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresaAerea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaAereaExists(empresaAerea.Id))
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
            return View(empresaAerea);
        }

        // GET: EmpresaAerea/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaAerea = await _context.EmpresasAereas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresaAerea == null)
            {
                return NotFound();
            }

            return View(empresaAerea);
        }

        // POST: EmpresaAerea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresaAerea = await _context.EmpresasAereas.FindAsync(id);
            if (empresaAerea != null)
            {
                _context.EmpresasAereas.Remove(empresaAerea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaAereaExists(int id)
        {
            return _context.EmpresasAereas.Any(e => e.Id == id);
        }
    }
}
