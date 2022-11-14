using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Concesionario;
using Models;

namespace Concesionario.Controllers
{
    public class AutoController : Controller
    {
        private readonly ProjectContext _context;

        public AutoController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Auto
        public async Task<IActionResult> Index()
        {
              return View(await _context.Auto.ToListAsync());
        }

        // GET: Auto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Auto == null)
            {
                return NotFound();
            }

            var auto = await _context.Auto
                .FirstOrDefaultAsync(m => m.Rowid == id);
            if (auto == null)
            {
                return NotFound();
            }

            return View(auto);
        }

        // GET: Auto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Auto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Placa,Marca,Modelo,Color,Rowid")] Auto auto)
        {
            if (ModelState.IsValid)
            {
                if(_context.Set<Auto>().Any(x => x.Placa.Equals(auto.Placa))){
                    return RedirectToAction(nameof(Index));
                }
                _context.Add(auto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(auto);
        }

        // GET: Auto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Auto == null)
            {
                return NotFound();
            }

            var auto = await _context.Auto.FindAsync(id);
            if (auto == null)
            {
                return NotFound();
            }
            return View(auto);
        }

        // POST: Auto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Placa,Marca,Modelo,Color,Rowid")] Auto auto)
        {
            if (id != auto.Rowid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(_context.Set<Auto>().Any(x => x.Placa.Equals(auto.Placa) && x.Rowid != auto.Rowid)){
                        return RedirectToAction(nameof(Index));
                    }
                    _context.Update(auto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoExists(auto.Rowid))
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
            return View(auto);
        }

        // GET: Auto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Auto == null)
            {
                return NotFound();
            }

            var auto = await _context.Auto
                .FirstOrDefaultAsync(m => m.Rowid == id);
            if (auto == null)
            {
                return NotFound();
            }

            return View(auto);
        }

        // POST: Auto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Auto == null)
            {
                return Problem("Entity set 'ProjectContext.Auto'  is null.");
            }
            var auto = await _context.Auto.FindAsync(id);
            if (auto != null)
            {
                _context.Auto.Remove(auto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoExists(int id)
        {
          return _context.Auto.Any(e => e.Rowid == id);
        }
    }
}
