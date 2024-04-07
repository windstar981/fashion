using fashion.Data;
using fashion.Models;
using Microsoft.EntityFrameworkCore;
using fashion.Service;
using MailKit;
using fashion.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

//builder.Services.AddTransient<EmailSettings>();
//builder.Services.AddTransient<EmailService>();
//builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(20*60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//builder.Services.AddSession(options =>
//{
//    options.Cookie.Name = ".AdventureWorks.Session";
//    options.IdleTimeout = TimeSpan.FromSeconds(10);
//    options.Cookie.IsEssential = true;
//});

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<FashionContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("fashion"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Brands}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "SendEmail",
    pattern: "Email/SendEmail",
    defaults: new { controller = "Email", action = "SendEmail" });

app.Run();
