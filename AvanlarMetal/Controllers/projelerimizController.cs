using Microsoft.AspNetCore.Mvc;

namespace AvanlarMetal.Controllers;

public class Projelerimiz : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}