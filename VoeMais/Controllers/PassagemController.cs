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
    public class PassagemController : Controller
    {
        private readonly VoeMaisContext _context;

        public PassagemController(VoeMaisContext context)
        {
            _context = context;
        }

        // GET: Passagem
        public async Task<IActionResult> Index()
        {
            var voeMaisContext = _context.Passagens.Include(p => p.Cliente).Include(p => p.VooPoltrona);
            return View(await voeMaisContext.ToListAsync());
        }

        // GET: Passagem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passagem = await _context.Passagens
                .Include(p => p.Cliente)
                .Include(p => p.VooPoltrona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passagem == null)
            {
                return NotFound();
            }

            return View(passagem);
        }

        // GET: Passagem/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id");
            ViewData["VooPoltronaId"] = new SelectList(_context.VoosPoltronas, "Id", "Id");
            return View();
        }

        // POST: Passagem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,VooPoltronaId,DataCompra")] Passagem passagem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", passagem.ClienteId);
            ViewData["VooPoltronaId"] = new SelectList(_context.VoosPoltronas, "Id", "Id", passagem.VooPoltronaId);
            return View(passagem);
        }

        // GET: Passagem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passagem = await _context.Passagens.FindAsync(id);
            if (passagem == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", passagem.ClienteId);
            ViewData["VooPoltronaId"] = new SelectList(_context.VoosPoltronas, "Id", "Id", passagem.VooPoltronaId);
            return View(passagem);
        }

        // POST: Passagem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,VooPoltronaId,DataCompra")] Passagem passagem)
        {
            if (id != passagem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassagemExists(passagem.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", passagem.ClienteId);
            ViewData["VooPoltronaId"] = new SelectList(_context.VoosPoltronas, "Id", "Id", passagem.VooPoltronaId);
            return View(passagem);
        }

        // GET: Passagem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passagem = await _context.Passagens
                .Include(p => p.Cliente)
                .Include(p => p.VooPoltrona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passagem == null)
            {
                return NotFound();
            }

            return View(passagem);
        }

        // POST: Passagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passagem = await _context.Passagens.FindAsync(id);
            if (passagem != null)
            {
                _context.Passagens.Remove(passagem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassagemExists(int id)
        {
            return _context.Passagens.Any(e => e.Id == id);
        }
    }
}
