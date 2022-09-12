using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.ViewModels;

namespace StackOverFlowImitationsProject.Controllers
{
    public class QuestionController : Controller
    {
        IQuestionService qs;
        ICategoriesService cs;
        IAnswersService asr;


        public QuestionController(IQuestionService qs, ICategoriesService cs, IAnswersService asr)
        {
            this.cs = cs;
            this.qs = qs;
            this.asr = asr;
        }
       
        public ActionResult View(int qID=1)
        {
            //have to update the view counter
            this.qs.UpdateQuestionViewsCount(qID, 1);
            int uID = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel questionViewModel = this.qs.GetQuestionByID(qID, uID);

           
            
                return View(questionViewModel);
            
            
            
        }
    }
}