using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AvanlarMetal.Models;

namespace AvanlarMetal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Anasayfa()
    {
        return View();
    }
    
}