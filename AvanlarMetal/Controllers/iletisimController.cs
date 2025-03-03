using Microsoft.AspNetCore.Mvc;

namespace AvanlarMetal.Controllers;

public class IletisimController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}