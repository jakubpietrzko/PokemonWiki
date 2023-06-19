using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokemonWiki.Data;
using PokemonWiki.Models;

namespace PokemonWiki.Controllers
{
    public class Type_pokController : Controller
    {
        private readonly PokemonWikiContext _context;

        public Type_pokController(PokemonWikiContext context)
        {
            _context = context;
        }

        // GET: Type_pok
        public async Task<IActionResult> Index()
        {
              return _context.Type_pok != null ? 
                          View(await _context.Type_pok.ToListAsync()) :
                          Problem("Entity set 'PokemonWikiContext.Type_pok'  is null.");
        }

        // GET: Type_pok/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Type_pok == null)
            {
                return NotFound();
            }

            var type_pok = await _context.Type_pok
                .FirstOrDefaultAsync(m => m.Id == id);
            if (type_pok == null)
            {
                return NotFound();
            }

            return View(type_pok);
        }

        // GET: Type_pok/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Type_pok/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName,Weaknesses,Strengths")] Type_pok type_pok)
        {
            if (ModelState.IsValid)
            {
                _context.Add(type_pok);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(type_pok);
        }

        // GET: Type_pok/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Type_pok == null)
            {
                return NotFound();
            }

            var type_pok = await _context.Type_pok.FindAsync(id);
            if (type_pok == null)
            {
                return NotFound();
            }
            return View(type_pok);
        }

        // POST: Type_pok/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName,Weaknesses,Strengths")] Type_pok type_pok)
        {
            if (id != type_pok.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(type_pok);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Type_pokExists(type_pok.Id))
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
            return View(type_pok);
        }

        // GET: Type_pok/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Type_pok == null)
            {
                return NotFound();
            }

            var type_pok = await _context.Type_pok
                .FirstOrDefaultAsync(m => m.Id == id);
            if (type_pok == null)
            {
                return NotFound();
            }

            return View(type_pok);
        }

        // POST: Type_pok/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Type_pok == null)
            {
                return Problem("Entity set 'PokemonWikiContext.Type_pok'  is null.");
            }
            var type_pok = await _context.Type_pok.FindAsync(id);
            if (type_pok != null)
            {
                _context.Type_pok.Remove(type_pok);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Type_pokExists(int id)
        {
          return (_context.Type_pok?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
