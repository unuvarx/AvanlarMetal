using Microsoft.AspNetCore.Mvc;

namespace AvanlarMetal.Controllers;

public class hakkimizdaController : Controller
{
    // GET
    public IActionResult index()
    {
        ViewBag.ActivePage = 1;
        return View();
    }
}