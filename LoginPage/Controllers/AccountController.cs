using LoginPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace LoginPage.Controllers
{
    public class AccountController : Controller
    {
        AccountDBEntities1 dc = new AccountDBEntities1();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {            
            var user = dc.Logins.SingleOrDefault(u => u.Name == login.Name);
            if (user != null)
            {
                if (user.Password == login.Password)
                {
                    return RedirectToAction("Success", "Account");
                }
                else
                {
                    Session["Msg"] = "Password Incorrect";
                    return View(login);
                }
            }
            return View(login);
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(Login login)
        {
            dc.Logins.Add(login);
            dc.SaveChanges();

            return RedirectToAction("Login");

        }
        public ActionResult Success()
        {
            var log = dc.Logins;
            return View(log);
        }
        public ViewResult Edit(int Id)
        {
            var log = dc.Logins.Find(Id);
            return View(log);
        }
        [HttpPost]
        public ActionResult Update(Login login)
        {
            dc.Entry(login).State = EntityState.Modified;
            dc.SaveChanges();
            return RedirectToAction("Success", "Account");
        }
    }
}