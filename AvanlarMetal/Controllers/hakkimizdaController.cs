using Microsoft.AspNetCore.Mvc;

namespace AvanlarMetal.Controllers;

public class HakkimizdaController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}