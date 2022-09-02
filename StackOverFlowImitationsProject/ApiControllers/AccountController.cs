using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.ViewModels;




namespace StackOverFlowImitationsProject.ApiControllers
{
    public class AccountController : ApiController
    {
        IUserservice us;

        //constructor

        public AccountController(IUserservice us)
        {
            this.us = us;//dependancy injection (mapped aleady in UnityConfig
        }

        public string Get(string Email)
        {
           UserViewModel user =  this.us.GetUserByEmail(Email);
            
            
            if(user != null)//user exists
            {
                return "Found";

            }
            else
            {
                return "Not Found";
            }
        }
    }
}
