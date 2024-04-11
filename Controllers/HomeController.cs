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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
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

                    string body = "Click <a href='http://localhost:5074/home/activation?email=" 
                        + customer.Email + "&code="+ randomNumber.ToString() +"'>here</a>" +
                        " to activation account!!!";

                    _context.Customers.Add(customer);
                    _context.SaveChanges();

                    // gui mail thong bao dang ky thanh cong
                    ViewBag.success = "Bạn đã đăng ký thành công vui lòng truy cập email để kích hoạt tài khoản!!";
                    MailService.SendRegistrationEmail(customer.Email, body);
                    return View("Login");
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
                var customer = await _context.Customers.FirstOrDefaultAsync(s => s.Email.Equals(login.Email) && s.StatusActive == 1);
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

       

        public async Task<IActionResult> Activation(string email, string code)
        {
            // kiểm tra email và password không được trống nếu trống thì trả về view kèm lỗi gì đó
            if (email.IsNullOrEmpty() || code.IsNullOrEmpty())
            {
                ViewBag.error = "Kích hoạt tài khoản không thành công!!";
                return View("Login");
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(s => s.Email.Equals(email) && s.ActivationCode == code);
            if (customer != null)
            {
                customer.StatusActive = 1;
                _context.SaveChanges();
                ViewBag.success = "Kích hoạt tài khoản thành công!!";
                return View("Login");
            }
            else
            {
                ViewBag.error = "Kích hoạt tài khoản không thành công!!";
            }

            return View("Login");
        }

        [HttpPost]
        public string Test([FromBody] dynamic data)
        {
            string name = data.GetProperty("name").GetString();
            return name;
        }

    }

}
