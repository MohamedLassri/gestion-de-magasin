using gestion_de_magasin.Data;
using gestion_de_magasin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gestion_de_magasin.Areas.Gerant.Controllers
{
    [Area("Gerant")]
    [Authorize(Roles = "Gerant,Admin")]
    public class FournisseursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FournisseursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gerant/Fournisseurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fournisseurs.ToListAsync());
        }

        // GET: Gerant/Fournisseurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseur = await _context.Fournisseurs
                .FirstOrDefaultAsync(m => m.FournisseurId == id);
            if (fournisseur == null)
            {
                return NotFound();
            }

            return View(fournisseur);
        }

        // GET: Gerant/Fournisseurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gerant/Fournisseurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FournisseurId,Nom,Telephone,Adresse")] Fournisseur fournisseur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fournisseur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fournisseur);
        }

        // GET: Gerant/Fournisseurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur == null)
            {
                return NotFound();
            }
            return View(fournisseur);
        }

        // POST: Gerant/Fournisseurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FournisseurId,Nom,Telephone,Adresse")] Fournisseur fournisseur)
        {
            if (id != fournisseur.FournisseurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fournisseur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FournisseurExists(fournisseur.FournisseurId))
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
            return View(fournisseur);
        }

        // GET: Gerant/Fournisseurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseur = await _context.Fournisseurs
                .FirstOrDefaultAsync(m => m.FournisseurId == id);
            if (fournisseur == null)
            {
                return NotFound();
            }

            return View(fournisseur);
        }

        // POST: Gerant/Fournisseurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur != null)
            {
                _context.Fournisseurs.Remove(fournisseur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FournisseurExists(int id)
        {
            return _context.Fournisseurs.Any(e => e.FournisseurId == id);
        }
    }
}
