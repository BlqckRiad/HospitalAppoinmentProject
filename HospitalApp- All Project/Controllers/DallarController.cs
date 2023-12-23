using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalApp.Models;
using Microsoft.Extensions.Localization;

namespace HospitalApp.Controllers
{
    public class DallarController : Controller
    {
        private readonly HospitalContext _context;
        public readonly IStringLocalizer<HomeController> _localization;
        public DallarController(HospitalContext context , IStringLocalizer<HomeController> localization)
        {
            _localization = localization;
            _context = context;
        }

        // GET: Dallar
        public async Task<IActionResult> Index()
        {
            var localizedTitles = _localization["Edit"];
            return _context.Dallar != null ? 
                          View(await _context.Dallar.ToListAsync()) :
                          Problem("Entity set 'HospitalContext.Dallar'  is null.");
        }

        // GET: Dallar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dallar == null)
            {
                return NotFound();
            }

            var dal = await _context.Dallar
                .FirstOrDefaultAsync(m => m.DalId == id);
            if (dal == null)
            {
                return NotFound();
            }

            return View(dal);
        }

        // GET: Dallar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dallar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DalId,DalName")] Dal dal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dal);
        }

        // GET: Dallar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dallar == null)
            {
                return NotFound();
            }

            var dal = await _context.Dallar.FindAsync(id);
            if (dal == null)
            {
                return NotFound();
            }
            return View(dal);
        }

        // POST: Dallar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DalId,DalName")] Dal dal)
        {
            if (id != dal.DalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DalExists(dal.DalId))
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
            return View(dal);
        }

        // GET: Dallar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dallar == null)
            {
                return NotFound();
            }

            var dal = await _context.Dallar
                .FirstOrDefaultAsync(m => m.DalId == id);
            if (dal == null)
            {
                return NotFound();
            }

            return View(dal);
        }

        // POST: Dallar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dallar == null)
            {
                return Problem("Entity set 'HospitalContext.Dallar'  is null.");
            }
            var dal = await _context.Dallar.FindAsync(id);
            if (dal != null)
            {
                _context.Dallar.Remove(dal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DalExists(int id)
        {
          return (_context.Dallar?.Any(e => e.DalId == id)).GetValueOrDefault();
        }
    }
}
