using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowProject.ViewModels;
using StackOverflowProject.ServiceLayer;


namespace StackOverFlowImitationsProject.Controllers
{
    public class AccountController : Controller
    {
        IUserservice us;

        //constructor with depenancy injection

        public AccountController(IUserservice us)
        {
            this.us = us;
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if(ModelState.IsValid)
            {
                int uid = this.us.InserUser(rvm);
                Session["CurrentUserID"]=uid;
                Session["CurrentUserName"] = rvm.Name;
                Session["CurrentUserEmail"]=rvm.Email;
                Session["CurrentUserPassword"] = rvm.Password;
                Session["CurrentUserIsAdmin"] = false;
                return RedirectToAction("index", "home");

            }
            else
            {
                ModelState.AddModelError("x", "invliad data");
                return View();
            }
            
        }
    }
}