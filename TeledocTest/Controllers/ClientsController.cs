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
    public class ClientsController : Controller
    {
        private readonly TeledocContext _context;

        public ClientsController(TeledocContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.Include(c => c.Founders).FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            ViewBag.AllFounders = _context.Founders.ToList();
            return View();
        }

        // POST: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Inn,Name,Type,CreateDate,UpdateDate")] Client client, int[] selectedFounders)
        {
            if (ModelState.IsValid)
            {
                client.Type = "Юридическое лицо";
                client.CreateDate = DateTime.Now;
                client.UpdateDate = DateTime.Now;
                if (selectedFounders != null)
                {
                    foreach (var f in _context.Founders.Where(fo => selectedFounders.Contains(fo.Id)))
                    {
                        client.Founders.Add(f);
                    }
                }
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/CreateIP
        public IActionResult CreateIP()
        {
            ViewBag.AllFounders = _context.Founders.ToList();
            return View();
        }

        // POST: Clients/CreateIP
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIP([Bind("Id,Inn,Name,Type,CreateDate,UpdateDate")] Client client, int[] selectedFounders)
        {
            if (ModelState.IsValid)
            {
                var newFownder = _context.Founders.Find(selectedFounders.First());
                client.Inn = newFownder.Inn;
                client.Name = "ИП " + newFownder.FullName;
                client.Type = "Индивидуальный предприниматель";
                client.CreateDate = DateTime.Now;
                client.UpdateDate = DateTime.Now;
                if (selectedFounders != null)
                {
                    foreach (var f in _context.Founders.Where(fo => selectedFounders.Contains(fo.Id)))
                    {
                        client.Founders.Add(f);
                    }
                }
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.Include(c => c.Founders).FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            ViewBag.AllFounders = _context.Founders.ToList();
            return View(client);
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Inn,Name,Type,CreateDate,UpdateDate")] Client client, int[] selectedFounders)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var newClient = await _context.Clients.Include(c => c.Founders).FirstOrDefaultAsync(m => m.Id == client.Id);
                    newClient.UpdateDate = DateTime.Now;
                    newClient.Founders.Clear();
                    newClient.Inn = client.Inn;
                    newClient.Name = client.Name;
                    if (selectedFounders != null)
                    {
                        foreach (var f in _context.Founders.Where(fo => selectedFounders.Contains(fo.Id)))
                        {
                            newClient.Founders.Add(f);
                        }
                    }
                    _context.Update(newClient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/EditIP/5
        public async Task<IActionResult> EditIP(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.Include(c => c.Founders).FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            ViewBag.AllFounders = _context.Founders.ToList();
            return View(client);
        }

        // POST: Clients/EditIP/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditIP(int id, [Bind("Id,Inn,Name,Type,CreateDate,UpdateDate")] Client client, int[] selectedFounders)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var newClient = await _context.Clients.Include(c => c.Founders).FirstOrDefaultAsync(m => m.Id == client.Id);
                    var newFownder = _context.Founders.Find(selectedFounders.First());
                    newClient.Inn = newFownder.Inn;
                    newClient.Name = "ИП " + newFownder.FullName;
                    newClient.UpdateDate = DateTime.Now;
                    newClient.Founders.Clear();
                    if (selectedFounders != null)
                    {
                        foreach (var f in _context.Founders.Where(fo => selectedFounders.Contains(fo.Id)))
                        {
                            newClient.Founders.Add(f);
                        }
                    }
                    _context.Update(newClient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
