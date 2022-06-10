using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Examen3.NET.Models;
using Examen3.NET.Data;
using Microsoft.EntityFrameworkCore;

namespace Examen3.NET.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
        {
              return _context.Bebidas != null ? 
                          View(await _context.Bebidas.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Bebidas'  is null.");
        }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
