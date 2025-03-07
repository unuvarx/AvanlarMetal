using AvanlarMetal.Models;
using Microsoft.AspNetCore.Mvc;

namespace AvanlarMetal.Controllers;

public class projelerimizController : Controller
{
    private readonly Contexts _contexts;

    public projelerimizController(Contexts contexts)
    {
        _contexts = contexts;
    }
    // GET
    public IActionResult index()
    {
        ViewBag.ActivePage = 3;
        ViewBag.Products = _contexts.Products.ToList();
        ViewBag.Categories = _contexts.Categories.ToList();
        return View();
    }

    public IActionResult detay(int id)
    {
        ViewBag.ActivePage = 3;
        return View();
    }
}