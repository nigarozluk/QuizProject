using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.DAL.Abstract;
using Project.DAL.Entity;
using Project.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private ILogicService _service;
        public AccountController(ILogicService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var user = _service.User.FindByCondition(x => x.UserMail == model.Mail && x.UserPassword==model.Password).FirstOrDefault();
            if (user == null)
            {
                TempData["msg"] = "User Not Exist!";
                return RedirectToAction("Index", "Account");
            }
            if (user.Role==RoleType.Admin)
            {
                HttpContext.Session.SetString("AdminSession", model.Mail);
                return RedirectToAction("Index", "Exam", new { area = "Admin" });
            }
            else
            {
                HttpContext.Session.SetString("UserSession", model.Mail);
                return RedirectToAction("Index", "Home");
            }
          
            
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Account");
        }
    }
}
