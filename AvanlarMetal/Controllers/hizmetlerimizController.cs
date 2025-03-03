using Microsoft.AspNetCore.Mvc;

namespace AvanlarMetal.Controllers;

public class HizmetlerimizController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}