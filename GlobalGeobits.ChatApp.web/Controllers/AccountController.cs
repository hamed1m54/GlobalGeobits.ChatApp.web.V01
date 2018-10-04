using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalGeobits.ChatApp.web.Models;

namespace GlobalGeobits.ChatApp.web.Controllers
{
    public class AccountController : Controller
    {
      

        public ActionResult Register()
        {
            if (Session["user_id"] != null)
            {
                return RedirectToAction("chat", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users account)
        {


            if (ModelState.IsValid)
            {

                using (ChatAppDbContext DB = new ChatAppDbContext())
                {

                    DB.Users.Add(account);
                    DB.SaveChanges();
                }
                ModelState.Clear();
                return Redirect("Thanks/" + account.UserFristName);
            }

            return View();

        }
           

        public ActionResult Login()
        {
            if (Session["user_id"] != null)
            {
                return RedirectToAction("chat", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users account) {

            using (ChatAppDbContext db = new ChatAppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserEmail == account.UserEmail && u.UserPassword == account.UserPassword);
                if (user != null)
                {

                    Session["user_id"] = user.UserID.ToString();

                    user.status = 1;
                    db.SaveChanges();
                    return RedirectToAction("chat", "Home");

                   


                }
                else {
                    ModelState.AddModelError("invaledlogin", "Email or password are not correct. Please, try again");
                
                }
            }
            return View();
        }


        public ActionResult Thanks(string id)
        {

          return  ViewBag.userName = id;
        }
              

      


        [AllowAnonymous]
        public string CheckUserName(string input)
        {

            return ((new ChatAppDbContext().Users.FirstOrDefault(u => u.UserEmail == input)) == null) ? "Available" : "Not Available";
        }

    }
}