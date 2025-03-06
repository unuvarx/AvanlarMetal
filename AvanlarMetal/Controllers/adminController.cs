
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AvanlarMetal.Models;
using Microsoft.IdentityModel.Tokens;



namespace AvanlarMetal.Controllers;

public class adminController : Controller
{
    private readonly Contexts _contexts;
    private readonly IConfiguration _configuration;
    private const string AuthCookieName = "_u";

    public adminController(Contexts contexts, IConfiguration configuration)
    {
        
        _contexts = contexts;
        _configuration = configuration;
    }
    
    
    private Users ValidateToken(string token)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
        var tokenHandler = new JwtSecurityTokenHandler();

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = secretKey,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var username = jwtToken.Subject;

        var user = _contexts.Users.FirstOrDefault(u => u.UserName == username);
        return user;
    }
    private bool UserIsAuthenticated()
    {
        try
        {
            string token = Request.Cookies[AuthCookieName];
            Console.WriteLine("ÇEREEEZZZ; : " + token);

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var user = ValidateToken(token);

            if (user == null)
            {
                return false;
            }


            return true;
        }
        catch (Exception ex)
        {

            return false;
        }
    }
    private void AddJwtCookie(string token)
    {
        Response.Cookies.Append(AuthCookieName, token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.Now.AddHours(1)
        });
    }

    
    
    private string CreateToken(Users user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        Console.WriteLine("TOKENEEEENNENE : " + secretKey);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    
    
    public IActionResult login()
    {
        return View();
    }
    
    [HttpPost]
    public ActionResult LoginReq(string username, string password)
    {
        try
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre alanı boş";
                return View("login");
            }

            var user = _contexts.Users.FirstOrDefault(u => u.UserName == username);
        
            if (user == null)
            {
                ViewBag.ErrorMessage = "Hatalı şifre veya kullanıcı adı!";
                return View("login");
            }

            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                ViewBag.ErrorMessage = "Kullanıcı bilgileri hatalı!";
                return View("login");
            }

            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                AddJwtCookie(CreateToken(user));
                return RedirectToAction("home", "admin");
            }
            else
            {
                ViewBag.ErrorMessage = "Hatalı şifre veya kullanıcı adı!";
                return View("login");
            }
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "Hatalı işlem!";
            Console.WriteLine(ex.Message);
            Console.WriteLine(username);
            Console.WriteLine(password);
            return View("login");
        }
    }

    public IActionResult home()
    {
        if (!UserIsAuthenticated())
        {
            return RedirectToAction("login", "admin");
        }
        return View();
    }

}