using Microsoft.AspNetCore.Mvc;

namespace AvanlarMetal.Controllers;

public class LazerKesimController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}