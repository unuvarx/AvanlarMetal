using AvanlarMetal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AvanlarMetal.Controllers;

public class iletisimController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly Contexts _contexts;

    public iletisimController(IConfiguration configuration, Contexts contexts)
    {
    
        _configuration = configuration;
        _contexts = contexts;
    }
    public IActionResult index()
    {
        ViewBag.SiteKey = _configuration["ReCaptcha:SiteKey"];
        ViewBag.ActivePage = 5;
        return View();
    }
    
    public async Task<IActionResult>  ContactReq(string nameSurname, string email, string subject, string message, string gRecaptchaResponse)
    {
        try
        {
            if (!await ValidateRecaptcha(gRecaptchaResponse))
            {
                ViewBag.ErrorMessage = "reCAPTCHA doğrulaması başarısız. Lütfen tekrar deneyin.";
                return View("iletisim"); 
            }

            var newMail = new Mails
            {
                NameSurname = nameSurname,
                Email = email,
                Subject = subject,
                Message = message,

            };

            await _contexts.Mails.AddAsync(newMail);
            await _contexts.SaveChangesAsync();
            ViewBag.SuccessMessage = "Mail başarıyla gönderildi. En kısa sürede sizinle iletişime geçeceğiz.";
            Console.WriteLine("GÖNDERİLDİİİİ");
            return View("iletisim");
        }
        catch (Exception e)
        {
            ViewBag.ErrorMessage = "Mail gönderilirken bir hata oluştu";
            Console.WriteLine("HAAAAATAAAA");
            Console.WriteLine(e);
            return View("iletisim");
        }
    }
    
    private async Task<bool> ValidateRecaptcha(string gRecaptchaResponse)
    {
        using (var client = new HttpClient())
        {
            string secretKey = _configuration["ReCaptcha:SecretKey"];
            var response = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={gRecaptchaResponse}", null);
            var json = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(json);
            return result.success == "true";
        }
    }

}