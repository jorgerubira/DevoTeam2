using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevoTeam.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DevoTeam.Controllers
{
    public class PruebaImagensController : Controller
    {
        private readonly Contexto _context;

        public PruebaImagensController(Contexto context)
        {
            _context = context;
        }

        // GET: PruebaImagens
        public async Task<IActionResult> Index()
        {
            return View(await _context.PruebaImagen.ToListAsync());
        }

        // GET: PruebaImagens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pruebaImagen = await _context.PruebaImagen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pruebaImagen == null)
            {
                return NotFound();
            }

            return View(pruebaImagen);
        }

        // GET: PruebaImagens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PruebaImagens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PruebaImagen pruebaImagen, IFormFile imagenSubir)
        {
            if (ModelState.IsValid)
            {
                if (imagenSubir != null) {
                    MemoryStream ms = new MemoryStream();
                    imagenSubir.CopyTo(ms);
                    pruebaImagen.Imagen = ms.ToArray();
                }

                _context.Add(pruebaImagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pruebaImagen);
        }

        // GET: PruebaImagens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pruebaImagen = await _context.PruebaImagen.FindAsync(id);
            if (pruebaImagen == null)
            {
                return NotFound();
            }
            return View(pruebaImagen);
        }

        // POST: PruebaImagens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Imagen")] PruebaImagen pruebaImagen)
        {
            if (id != pruebaImagen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pruebaImagen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PruebaImagenExists(pruebaImagen.Id))
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
            return View(pruebaImagen);
        }

        // GET: PruebaImagens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pruebaImagen = await _context.PruebaImagen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pruebaImagen == null)
            {
                return NotFound();
            }

            return View(pruebaImagen);
        }

        // POST: PruebaImagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pruebaImagen = await _context.PruebaImagen.FindAsync(id);
            _context.PruebaImagen.Remove(pruebaImagen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PruebaImagenExists(int id)
        {
            return _context.PruebaImagen.Any(e => e.Id == id);
        }

        public async Task<IActionResult> MostrarImagen(int id)
        {
            PruebaImagen dato = await _context.PruebaImagen.FindAsync(id);
            return File(dato.Imagen, "image/png");
        }
    }
}

