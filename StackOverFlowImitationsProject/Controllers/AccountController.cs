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
    }
}