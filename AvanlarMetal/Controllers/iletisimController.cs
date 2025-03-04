using Microsoft.AspNetCore.Mvc;

namespace AvanlarMetal.Controllers;

public class iletisimController : Controller
{
    // GET
    public IActionResult index()
    {
        ViewBag.ActivePage = 5;
        return View();
    }
}