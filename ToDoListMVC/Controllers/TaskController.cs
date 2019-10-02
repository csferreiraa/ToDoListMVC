using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ToDoListMVC.Database;
using ToDoListMVC.Models;

namespace ToDoListMVC.Controllers
{
    public class TaskController : Controller
    {
        private readonly ToDoListContext _context;

        public TaskController(ToDoListContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Tasks.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var tasks = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tasks == null)
                return NotFound();
            return View(tasks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Tasks tasks)
        {
            if (!ModelState.IsValid)
                return View();

            _context.Add(tasks);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var tasks = await _context.Tasks.FindAsync(id);

            if (tasks == null)
                return NotFound();

            return View(tasks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tasks tasks)
        {
            if (id != tasks.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.Id))
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

            return View(tasks);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var tasks = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tasks == null)
                return NotFound();

            return View(tasks);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);

            _context.Tasks.Remove(tasks);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Download()
        {
            var teste = await _context.Tasks.ToListAsync();

            using (var stream = new MemoryStream())
            {
                using (var writeFile = new StreamWriter(stream))
                {
                    var csv = new CsvWriter(writeFile);

                    csv.WriteRecords(teste);
                    //Limpa todos os buffers para o gravador atual e faz com que todos os dados armazenados em buffer sejam gravados no fluxo subjacente.
                    writeFile.Flush();
                    return File(stream.ToArray(), "application/octet-stream", "Tasks.csv");
                }
            }
        }
    }

}
