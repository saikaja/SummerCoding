using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NIA.OnlineApp.Data;
using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.InteractiveAPI.Controllers
{
    public class TypeUtilController : Controller
    {
        private readonly AppDbContext _context;

        public TypeUtilController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TypeUtil
        public async Task<IActionResult> Index()
        {
            return View(await _context.Types.ToListAsync());
        }

        // GET: TypeUtil/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeUtil = await _context.Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeUtil == null)
            {
                return NotFound();
            }

            return View(typeUtil);
        }

        // GET: TypeUtil/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeUtil/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,IsActive")] TypeUtil typeUtil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeUtil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeUtil);
        }

        // GET: TypeUtil/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeUtil = await _context.Types.FindAsync(id);
            if (typeUtil == null)
            {
                return NotFound();
            }
            return View(typeUtil);
        }

        // POST: TypeUtil/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,IsActive")] TypeUtil typeUtil)
        {
            if (id != typeUtil.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeUtil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeUtilExists(typeUtil.Id))
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
            return View(typeUtil);
        }

        // GET: TypeUtil/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeUtil = await _context.Types
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeUtil == null)
            {
                return NotFound();
            }

            return View(typeUtil);
        }

        // POST: TypeUtil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeUtil = await _context.Types.FindAsync(id);
            if (typeUtil != null)
            {
                _context.Types.Remove(typeUtil);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeUtilExists(int id)
        {
            return _context.Types.Any(e => e.Id == id);
        }
    }
}
