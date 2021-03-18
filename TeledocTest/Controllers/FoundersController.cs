using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeledocTest.Models;

namespace TeledocTest.Controllers
{
    public class FoundersController : Controller
    {
        private readonly TeledocContext _context;

        public FoundersController(TeledocContext context)
        {
            _context = context;
        }

        // GET: Founders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Founders.ToListAsync());
        }

        // GET: Founders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var founder = await _context.Founders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (founder == null)
            {
                return NotFound();
            }

            return View(founder);
        }

        // GET: Founders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Founders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Inn,FirstName,LastName,FullName,SecondName,CreateDate,UpdateDate")] Founder founder)
        {
            if (ModelState.IsValid)
            {
                founder.SetFullName();
                founder.CreateDate = DateTime.Now;
                founder.UpdateDate = DateTime.Now;
                _context.Add(founder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(founder);
        }

        // GET: Founders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var founder = await _context.Founders.FindAsync(id);
            if (founder == null)
            {
                return NotFound();
            }
            return View(founder);
        }

        // POST: Founders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Inn,FirstName,LastName,SecondName,CreateDate,UpdateDate")] Founder founder)
        {
            if (id != founder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    founder.SetFullName();
                    founder.UpdateDate = DateTime.Now;
                    _context.Update(founder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FounderExists(founder.Id))
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
            return View(founder);
        }

        // GET: Founders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var founder = await _context.Founders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (founder == null)
            {
                return NotFound();
            }

            return View(founder);
        }

        // POST: Founders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var founder = await _context.Founders.FindAsync(id);
            _context.Founders.Remove(founder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FounderExists(int id)
        {
            return _context.Founders.Any(e => e.Id == id);
        }
    }
}
