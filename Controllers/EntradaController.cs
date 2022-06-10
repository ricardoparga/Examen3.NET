using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examen3.NET.Data;
using Examen3.NET.Models;

namespace Examen3.NET.Controllers
{
    public class EntradaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EntradaController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Entrada
        public async Task<IActionResult> Index()
        {
              return _context.Entradas != null ? 
                          View(await _context.Entradas.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Entradas'  is null.");
        }

        // GET: Entrada/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Entradas == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // GET: Entrada/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entrada/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio")] Entrada entrada)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (archivos.Count()>0){
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\entradas\");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(subidas,nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStream);
                    }
                    entrada.UrlImagen = @"imagenes\entradas\" + nombreArchivo + extension;
                }
                _context.Add(entrada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entrada);
        }

        // GET: Entrada/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Entradas == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas.FindAsync(id);
            if (entrada == null)
            {
                return NotFound();
            }
            return View(entrada);
        }

        // POST: Entrada/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio")] Entrada entrada)
        {
            if (id != entrada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string rutaPrincipal = _hostEnvironment.WebRootPath;
                    var archivos = HttpContext.Request.Form.Files;
                    if (archivos.Count()>0){
                        Entrada? entradaBD = await  _context.Entradas.FindAsync(id);
                        if(entradaBD != null){
                            if(entradaBD.UrlImagen!=null){
                                var rutaImagenActual = Path.Combine(rutaPrincipal,entradaBD.UrlImagen);
                                if(System.IO.File.Exists(rutaImagenActual)){
                                    System.IO.File.Delete(rutaImagenActual);
                                }
                            }
                            _context.Entry(entradaBD).State = EntityState.Detached;                              
                        }
                        string nombreArchivo = Guid.NewGuid().ToString();
                        var subidas = Path.Combine(rutaPrincipal, @"imagenes\entradas\");
                        var extension = Path.GetExtension(archivos[0].FileName);
                        using (var fileStream = new FileStream(Path.Combine(subidas,nombreArchivo + extension), FileMode.Create)){
                            archivos[0].CopyTo(fileStream);
                        }
                        entrada.UrlImagen = @"imagenes\entradas\" + nombreArchivo + extension;
                        _context.Entry(entrada).State = EntityState.Modified;
                    }
                    _context.Update(entrada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntradaExists(entrada.Id))
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
            return View(entrada);
        }

        // GET: Entrada/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Entradas == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // POST: Entrada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Entradas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Entradas'  is null.");
            }
            var entrada = await _context.Entradas.FindAsync(id);
            if (entrada != null)
            {
                _context.Entradas.Remove(entrada);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntradaExists(int id)
        {
          return (_context.Entradas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
