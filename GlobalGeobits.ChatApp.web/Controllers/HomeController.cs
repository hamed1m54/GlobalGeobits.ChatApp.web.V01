using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalGeobits.ChatApp.web.Models;

namespace GlobalGeobits.ChatApp.web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Chat(int id = -1)
        {

            if (Session["user_id"] != null)
            {
                if (id != -1)
                {
                    ViewBag.id = id;
                    var incaht = new ChatAppDbContext().Users.FirstOrDefault(u => u.UserID == id);
                    if (incaht != null)
                        ViewBag.chatName = incaht.UserDisplayName;
                }


                int id1 = int.Parse(Session["user_id"].ToString());
                var user = new ChatAppDbContext().Users.FirstOrDefault(u => u.UserID == id1);
                if (user != null)
                {
                    string DisplayName = user.UserDisplayName;
                    if (DisplayName == string.Empty)
                        DisplayName = user.UserFristName + " " + user.UserLastName;
                    ViewBag.DisplayName = DisplayName;
                }



                // get all users from database except current user in sesstion
                List<Users> users = new ChatAppDbContext().Users.Where(u => u.UserID != id1).ToList();
                return View(users); 
            }
            return RedirectToAction("login", "Account");
        }

       

        public ActionResult logout()
        {

            int id = int.Parse(Session["user_id"].ToString());
            using (ChatAppDbContext db = new ChatAppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserID == id);
                if (user != null)
                {
                    user.status = 0;
                    db.SaveChanges();
                }
            }


            Session["user_id"] = null;
            return RedirectToAction("login", "account");
        }


        public ActionResult startconversation(int? id = -1)
        {
         return   RedirectToAction("chat", new { id = id });
        }
       


    }
}