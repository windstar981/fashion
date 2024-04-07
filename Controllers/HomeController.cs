using fashion.Data;
using fashion.Models;
using fashion.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
namespace fashion.Controllers
{
    public class HomeController : Controller
    {
        private readonly FashionContext _context;
        //private readonly EmailService _emailService;


        public HomeController(FashionContext context/*, EmailService emailService*/)
        {
            _context = context;
            //_emailService = emailService;
        }
        public IActionResult Index() 
        {
            var trendyProducts = GetTrendyProducts();
            ViewBag.TrendyProducts = trendyProducts;

            return View();
        }

        private List<Product> GetTrendyProducts()
        {
            var trendyProducts = _context.Products.Include(p => p.Brand).ToList();
            return trendyProducts;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Register()
        {
            // Kiểm tra nếu đã đăng nhập
            if (HttpContext.Session.GetInt32("IdCustomer") != null)
            {
                // Đã đăng nhập, chuyển hướng đến trang chính
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Customers.FirstOrDefault(c => c.Email == customer.Email);
                if (check == null)
                {
                    customer.Pd = PasswordHasher.HashPassword(customer.Pd);
                    // tạo mã kích hoạt
                    var rand = new Random();

                    int randomNumber = rand.Next(1000, 10000);

                    customer.ActivationCode = randomNumber.ToString();

                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                    // gui mail thong bao dang ky thanh cong
                    MailService.SendRegistrationEmail(customer.Email);
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            //Kiểm tra nếu đã đăng nhập
            if (HttpContext.Session.GetInt32("IdCustomer") != null)
            {
                //Đã đăng nhập, chuyển hướng đến trang chính
                return RedirectToAction("Index");
            }
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(string email, string password)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var f_password = PasswordHasher.HashPassword(password);
        //        Console.WriteLine(f_password);
        //        var customer = await _context.Customers.FirstOrDefaultAsync(s => s.Email.Equals(email));
        //        if (customer != null)
        //        {
        //            // So sánh mật khẩu đã hash từ mật khẩu nhập vào với mật khẩu đã hash trong cơ sở dữ liệu
        //            if (PasswordHasher.VerifyPassword(customer.Pd, password))
        //            {
        //                // Thành công đăng nhập, lưu thông tin vào Session
        //                HttpContext.Session.SetString("FullName", customer.Name);
        //                HttpContext.Session.SetString("Email", customer.Email);
        //                HttpContext.Session.SetString("IdCustomer", customer.Id.ToString());
        //                return RedirectToAction("Index");
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.error = "Login failed";
        //            return RedirectToAction("Login");
        //        }
        //    }
        //    return View("Login");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(s => s.Email.Equals(login.Email));
                if (customer != null)
                {
                    if (PasswordHasher.VerifyPassword(customer.Pd, login.Password))
                    {
                        HttpContext.Session.SetString("FullName", customer.Name);
                        HttpContext.Session.SetString("Email", customer.Email);
                        HttpContext.Session.SetInt32("IdCustomer", customer.Id);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            ViewBag.error = "Login failed";
            return View("Login");
        }


        public IActionResult Logout()
        {
            // Xóa các thông tin đăng nhập từ Session
            HttpContext.Session.Clear();

            // Chuyển hướng người dùng đến trang đăng nhập
            return RedirectToAction("Login", "Home");
        }

    }
}
