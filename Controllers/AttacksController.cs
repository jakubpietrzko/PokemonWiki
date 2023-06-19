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
    public class AttacksController : Controller
    {
        private readonly PokemonWikiContext _context;

        public AttacksController(PokemonWikiContext context)
        {
            _context = context;
        }

        // GET: Attacks
        public async Task<IActionResult> Index()
        {
            var pokemonWikiContext = _context.Attacks.Include(a => a.Type);
            var attacksList = await pokemonWikiContext.ToListAsync();

            // Zamień ID typu na jego nazwę
            foreach (var attack in attacksList)
            {
                attack.Type = _context.Type_pok.FirstOrDefault(t => t.Id == attack.TypeId);
            }

            return View(attacksList);
        }

        // GET: Attacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Attacks == null)
            {
                return NotFound();
            }

            var attacks = await _context.Attacks
                .Include(a => a.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attacks == null)
            {
                return NotFound();
            }

            // Zamień ID typu na jego nazwę
            attacks.Type = _context.Type_pok.FirstOrDefault(t => t.Id == attacks.TypeId);

            return View(attacks);
        }

        // GET: Attacks/Create
        public IActionResult Create()
        {
            ViewData["Types"] = new SelectList(_context.Type_pok, "Id", "TypeName");
            return View();
        }

        // POST: Attacks/Create
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Id,AttackName,Power,TypeId")] Attacks attacks)
{
    if (ModelState.IsValid)
    {
        // Pobierz obiekt typu na podstawie wybranego ID
        attacks.Type = await _context.Type_pok.FindAsync(attacks.TypeId);

        _context.Add(attacks);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    ViewData["Types"] = new SelectList(_context.Type_pok, "Id", "TypeName", attacks.TypeId);
    return View(attacks);
}

        // GET: Attacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Attacks == null)
            {
                return NotFound();
            }

            var attacks = await _context.Attacks.FindAsync(id);
            if (attacks == null)
            {
                return NotFound();
            }
            ViewData["Types"] = new SelectList(_context.Type_pok, "Id", "TypeName", attacks.TypeId);
            return View(attacks);
        }

        // POST: Attacks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AttackName,Power,TypeId")] Attacks attacks)
        {
            if (id != attacks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                attacks.Type = _context.Type_pok.FirstOrDefault(t => t.Id == attacks.TypeId);
                try
                {
                    _context.Update(attacks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttacksExists(attacks.Id))
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
            ViewData["Types"] = new SelectList(_context.Type_pok, "Id", "TypeName", attacks.TypeId);
            return View(attacks);
        }

        // GET: Attacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attacks == null)
            {
                return NotFound();
            }

            var attacks = await _context.Attacks
                .Include(a => a.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attacks == null)
            {
                return NotFound();
            }

            return View(attacks);
        }

        // POST: Attacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attacks == null)
            {
                return Problem("Entity set 'PokemonWikiContext.Attacks' is null.");
            }
            var attacks = await _context.Attacks.FindAsync(id);
            if (attacks != null)
            {
                _context.Attacks.Remove(attacks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttacksExists(int id)
        {
            return (_context.Attacks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
