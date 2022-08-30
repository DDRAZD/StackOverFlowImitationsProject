using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlowProject.ServiceLayer;
using StackOverflowProject.ViewModels;



namespace StackOverFlowImitationsProject.Controllers
{
    public class HomeController : Controller
    {
        IQuestionService qs;


        //constructor

        public HomeController(IQuestionService qs)
        {
            this.qs = qs; //dependnacy injection
        }

        // GET: Home
        public ActionResult Index()
        {
            //dsiplaying the latest questions
            List<QuestionViewModel> questions =  this.qs.GetAllQuestions().Take(10).ToList(); //taking just the top 10
            return View(questions);
        }
    }
}