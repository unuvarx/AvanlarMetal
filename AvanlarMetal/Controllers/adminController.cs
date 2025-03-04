using Microsoft.AspNetCore.Mvc;

namespace AvanlarMetal.Controllers;

public class adminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}