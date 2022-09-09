using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.ViewModels;



namespace StackOverFlowImitationsProject.Controllers
{
    public class HomeController : Controller
    {
        IQuestionService qs;
        ICategoriesService cs;


        //constructor

        public HomeController(IQuestionService qs, ICategoriesService cs)
        {
            this.qs = qs; //dependnacy injection
            this.cs = cs;
        }

        // GET: Home
        public ActionResult Index()
        {
            //dsiplaying the latest questions
            List<QuestionViewModel> questions = this.qs.GetAllQuestions().Take(10).ToList(); //taking just the top 10
            return View(questions);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Categories()
        {
            List<CategoryViewModel> categories = this.cs.GetAllCategories();

            return View(categories);
        }

        [Route("allquestions")]
        public ActionResult Questions()
        {
            List<QuestionViewModel> questions = this.qs.GetAllQuestions();
            return View(questions);
        }
        /// <summary>
        /// searches questions; str is named in the layout page when the search box is implemented
        /// </summary>
        /// <param name="str"></param> 
        /// <returns></returns>
        public ActionResult Search(string str)
        {
            List<QuestionViewModel> questions = this.qs.GetAllQuestions().Where(
                x =>
                x.QuestionName.ToLower().Contains(str.ToLower())||
                x.Category.CategoryName.ToLower().Contains(str.ToLower())).ToList(); //tolower makes it case insensitive

            ViewBag.str = str;

            return View(questions);
        }


    }
}