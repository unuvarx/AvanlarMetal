using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AvanlarMetal.Models;

namespace AvanlarMetal.Controllers;

public class anasayfaController : Controller
{
    private readonly ILogger<anasayfaController> _logger;
    private readonly Contexts _contexts;

    public anasayfaController(ILogger<anasayfaController> logger, Contexts contexts)
    {
        
        _logger = logger;
        _contexts = contexts;
    }

    public IActionResult index()
    {
        ViewBag.ActivePage = 0;
        ViewBag.Products = _contexts.Products.ToList();
        ViewBag.Categories = _contexts.Categories.ToList();
        return View();
    }
    
}