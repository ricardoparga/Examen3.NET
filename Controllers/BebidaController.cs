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
    public class BebidaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public BebidaController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Bebida
        public async Task<IActionResult> Index()
        {
              return _context.Bebidas != null ? 
                          View(await _context.Bebidas.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Bebidas'  is null.");
        }

        // GET: Bebida/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bebidas == null)
            {
                return NotFound();
            }

            var bebida = await _context.Bebidas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bebida == null)
            {
                return NotFound();
            }

            return View(bebida);
        }

        // GET: Bebida/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bebida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio")] Bebida bebida)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (archivos.Count()>0){
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\bebidas\");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(subidas,nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStream);
                    }
                    bebida.UrlImagen = @"imagenes\bebidas\" + nombreArchivo + extension;
                }
                _context.Add(bebida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bebida);
        }

        // GET: Bebida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bebidas == null)
            {
                return NotFound();
            }

            var bebida = await _context.Bebidas.FindAsync(id);
            if (bebida == null)
            {
                return NotFound();
            }
            return View(bebida);
        }

        // POST: Bebida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio")] Bebida bebida)
        {
            if (id != bebida.Id)
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
                        Bebida? bebidaBD = await  _context.Bebidas.FindAsync(id);
                        if(bebidaBD != null){
                            if(bebidaBD.UrlImagen!=null){
                                var rutaImagenActual = Path.Combine(rutaPrincipal,bebidaBD.UrlImagen);
                                if(System.IO.File.Exists(rutaImagenActual)){
                                    System.IO.File.Delete(rutaImagenActual);
                                }
                            }
                            _context.Entry(bebidaBD).State = EntityState.Detached;                              
                        }
                        string nombreArchivo = Guid.NewGuid().ToString();
                        var subidas = Path.Combine(rutaPrincipal, @"imagenes\bebidas\");
                        var extension = Path.GetExtension(archivos[0].FileName);
                        using (var fileStream = new FileStream(Path.Combine(subidas,nombreArchivo + extension), FileMode.Create)){
                            archivos[0].CopyTo(fileStream);
                        }
                        bebida.UrlImagen = @"imagenes\bebidas\" + nombreArchivo + extension;
                        _context.Entry(bebida).State = EntityState.Modified;
                    }
                    _context.Update(bebida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BebidaExists(bebida.Id))
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
            return View(bebida);
        }

        // GET: Bebida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bebidas == null)
            {
                return NotFound();
            }

            var bebida = await _context.Bebidas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bebida == null)
            {
                return NotFound();
            }

            return View(bebida);
        }

        // POST: Bebida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bebidas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bebidas'  is null.");
            }
            var bebida = await _context.Bebidas.FindAsync(id);
            if (bebida != null)
            {
                _context.Bebidas.Remove(bebida);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BebidaExists(int id)
        {
          return (_context.Bebidas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
