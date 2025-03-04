using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AvanlarMetal.Models;

namespace AvanlarMetal.Controllers;

public class anasayfaController : Controller
{
    private readonly ILogger<anasayfaController> _logger;

    public anasayfaController(ILogger<anasayfaController> logger)
    {
        _logger = logger;
    }

    public IActionResult index()
    {
        ViewBag.ActivePage = 0;
        return View();
    }
    
}