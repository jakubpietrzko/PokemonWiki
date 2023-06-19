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
    public class PokemonController : Controller
    {
        private readonly PokemonWikiContext _context;

        public PokemonController(PokemonWikiContext context)
        {
            _context = context;
        }

       // GET: Pokemon
        public async Task<IActionResult> Index()
        {
            var pokemonList = await _context.Pokemon
                .Include(p => p.Type)
                .Include(p => p.Attack)
                .ToListAsync();

            // Zamień ID typu na jego nazwę
            foreach (var pokemon in pokemonList)
            {
                pokemon.Type = _context.Type_pok.FirstOrDefault(t => t.Id == pokemon.TypeId);
            }

            // Zamień ID ataku na jego nazwę
            foreach (var pokemon in pokemonList)
            {
                pokemon.Attack = _context.Attacks.FirstOrDefault(a => a.Id == pokemon.AttackId);
            }

            return View(pokemonList);
        }

        // GET: Pokemon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pokemon == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .Include(p => p.Attack)
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }
// GET: Pokemon/Create
public IActionResult Create()
{
    ViewData["TypeId"] = new SelectList(_context.Type_pok, "Id", "TypeName");
    ViewData["AttackId"] = new SelectList(_context.Attacks, "Id", "AttackName");
    return View();
}

// POST: Pokemon/Create
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Id,Name,TypeId,AttackId")] Pokemon pokemon)
{
    if (ModelState.IsValid)
    {
        _context.Add(pokemon);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    ViewData["TypeId"] = new SelectList(_context.Type_pok, "Id", "TypeName", pokemon.TypeId);
    ViewData["AttackId"] = new SelectList(_context.Attacks, "Id", "AttackName", pokemon.AttackId);
    return View(pokemon);
}


        // GET: Pokemon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pokemon == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            ViewData["AttackId"] = new SelectList(_context.Set<Attacks>(), "Id", "Id", pokemon.AttackId);
            ViewData["TypeId"] = new SelectList(_context.Set<Type_pok>(), "Id", "Id", pokemon.TypeId);
            return View(pokemon);
        }

        // POST: Pokemon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeId,AttackId")] Pokemon pokemon)
        {
            if (id != pokemon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonExists(pokemon.Id))
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
            ViewData["AttackId"] = new SelectList(_context.Set<Attacks>(), "Id", "Id", pokemon.AttackId);
            ViewData["TypeId"] = new SelectList(_context.Set<Type_pok>(), "Id", "Id", pokemon.TypeId);
            return View(pokemon);
        }

        // GET: Pokemon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pokemon == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .Include(p => p.Attack)
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // POST: Pokemon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pokemon == null)
            {
                return Problem("Entity set 'PokemonWikiContext.Pokemon'  is null.");
            }
            var pokemon = await _context.Pokemon.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemon.Remove(pokemon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonExists(int id)
        {
          return (_context.Pokemon?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
