using Microsoft.AspNetCore.Mvc;

namespace AvanlarMetal.Controllers;

public class projelerimizController : Controller
{
    // GET
    public IActionResult index()
    {
        ViewBag.ActivePage = 3;
        return View();
    }

    public IActionResult detay(int id)
    {
        ViewBag.ActivePage = 3;
        return View();
    }
}