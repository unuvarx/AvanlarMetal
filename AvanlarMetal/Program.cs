using AvanlarMetal.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<Contexts>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AvanlarMetalDB"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();


app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=anasayfa}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();