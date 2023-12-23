using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace HospitalApp.Controllers
{

    [Authorize(Roles = "admin")]
    public class DoktorsController : Controller
    {
        private readonly HospitalContext _context;

        public DoktorsController(HospitalContext context)
        {
            _context = context;
        }

        // GET: Doktors
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.Doktorlar.Include(d => d.Dal).Include(d => d.Poliklinik);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: Doktors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doktorlar == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktorlar
                .Include(d => d.Dal)
                .Include(d => d.Poliklinik)
                .FirstOrDefaultAsync(m => m.DoktorId == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // GET: Doktors/Create
        public IActionResult Create()
        {
            ViewBag.Dallar = new SelectList(_context.Dallar, "DalId", "DalName");
            ViewBag.Poliklinikler = new SelectList(_context.Poliklinikler, "PolId", "PolName");

            return View();
        }


        // POST: Doktors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoktorId,DoktorAd,DalId,PolId")] Doktor doktor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doktor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DalId"] = new SelectList(_context.Dallar, "DalId", "DalId", doktor.DalId);
            ViewData["PolId"] = new SelectList(_context.Poliklinikler, "PolId", "PolId", doktor.PolId);
            return View(doktor);
        }

        // GET: Doktors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktorlar
                .Include(d => d.Dal)
                .Include(d => d.Poliklinik)
                .FirstOrDefaultAsync(d => d.DoktorId == id);

            if (doktor == null)
            {
                return NotFound();
            }

            // Dallar ve Poliklinikler listelerini doldurun
            ViewData["DalId"] = new SelectList(_context.Dallar, "DalId", "DalName", doktor.DalId);
            ViewData["PolId"] = new SelectList(_context.Poliklinikler, "PolId", "PolName", doktor.PolId);

            return View(doktor);
        }

        // POST: Doktors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoktorId,DoktorAd,DalId,PolId")] Doktor doktor)
        {
            if (id != doktor.DoktorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doktor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoktorExists(doktor.DoktorId))
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
            ViewData["DalId"] = new SelectList(_context.Dallar, "DalId", "DalId", doktor.DalId);
            ViewData["PolId"] = new SelectList(_context.Poliklinikler, "PolId", "PolId", doktor.PolId);
            return View(doktor);
        }

        // GET: Doktors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doktorlar == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktorlar
                .Include(d => d.Dal)
                .Include(d => d.Poliklinik)
                .FirstOrDefaultAsync(m => m.DoktorId == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // POST: Doktors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doktorlar == null)
            {
                return Problem("Entity set 'HospitalContext.Doktorlar'  is null.");
            }
            var doktor = await _context.Doktorlar.FindAsync(id);
            if (doktor != null)
            {
                _context.Doktorlar.Remove(doktor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoktorExists(int id)
        {
          return (_context.Doktorlar?.Any(e => e.DoktorId == id)).GetValueOrDefault();
        }
    }
}
