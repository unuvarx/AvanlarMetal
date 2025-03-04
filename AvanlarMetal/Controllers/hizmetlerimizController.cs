using Microsoft.AspNetCore.Mvc;

namespace AvanlarMetal.Controllers;

public class hizmetlerimizController : Controller
{
    // GET
    public IActionResult index()
    {
        ViewBag.ActivePage = 2;
        return View();
    }
}