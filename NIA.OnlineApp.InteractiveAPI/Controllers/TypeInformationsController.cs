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
    public class TypeInformationsController : Controller
    {
        private readonly AppDbContext _context;

        public TypeInformationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TypeInformations
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TypeInformations.Include(t => t.TypeUtil);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TypeInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeInformation = await _context.TypeInformations
                .Include(t => t.TypeUtil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeInformation == null)
            {
                return NotFound();
            }

            return View(typeInformation);
        }

        // GET: TypeInformations/Create
        public IActionResult Create()
        {
            ViewData["Type_Id"] = new SelectList(_context.Types, "Id", "Type");
            return View();
        }

        // POST: TypeInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type_Id,Name,Value")] TypeInformation typeInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Type_Id"] = new SelectList(_context.Types, "Id", "Type", typeInformation.Type_Id);
            return View(typeInformation);
        }

        // GET: TypeInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeInformation = await _context.TypeInformations.FindAsync(id);
            if (typeInformation == null)
            {
                return NotFound();
            }
            ViewData["Type_Id"] = new SelectList(_context.Types, "Id", "Type", typeInformation.Type_Id);
            return View(typeInformation);
        }

        // POST: TypeInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type_Id,Name,Value")] TypeInformation typeInformation)
        {
            if (id != typeInformation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeInformationExists(typeInformation.Id))
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
            ViewData["Type_Id"] = new SelectList(_context.Types, "Id", "Type", typeInformation.Type_Id);
            return View(typeInformation);
        }

        // GET: TypeInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeInformation = await _context.TypeInformations
                .Include(t => t.TypeUtil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeInformation == null)
            {
                return NotFound();
            }

            return View(typeInformation);
        }

        // POST: TypeInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeInformation = await _context.TypeInformations.FindAsync(id);
            if (typeInformation != null)
            {
                _context.TypeInformations.Remove(typeInformation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeInformationExists(int id)
        {
            return _context.TypeInformations.Any(e => e.Id == id);
        }
    }
}
