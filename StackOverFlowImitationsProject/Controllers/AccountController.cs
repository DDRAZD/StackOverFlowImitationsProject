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

        public  ActionResult Login()
        {
            
            LoginViewModel lvm = new LoginViewModel(); //creating an empty one and going to transfer it to view to be filled
            
            return View(lvm);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel lvm)
        {
            if(ModelState.IsValid) //this is checked with data annotations autmoatically at the lvm level (LoginViewModel.cs)
            {
                UserViewModel uvm = this.us.GetUserByEmailandPassword(lvm.Email, lvm.Password);
                if(uvm != null)
                {

                    Session["CurrentUserID"] = uvm.UserID;
                    Session["CurrentUserName"] = uvm.Name;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserPassword"] = uvm.Password;
                    Session["CurrentUserIsAdmin"] = uvm.IsAdmin;

                    if(uvm.IsAdmin==true)
                    {
                        return RedirectToRoute(new { area = "admin", controller = "AdminHome", action = "Index" });
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }

                }
                else
                {
                    ModelState.AddModelError("x", "invliad EMail /Password"); //this will appear in the html validation summary
                    return View(lvm);
                }                    

            }
            else
            {
                ModelState.AddModelError("x", "invalid data");//this will appear in the html validation summary
                return View(lvm);
            }

         

        }
    }
}