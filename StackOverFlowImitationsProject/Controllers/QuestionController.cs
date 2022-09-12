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
       
        public ActionResult View(int qID=0)
        {
            //have to update the view counter
            this.qs.UpdateQuestionViewsCount(qID, 1);
            int uID = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel questionViewModel = this.qs.GetQuestionByID(qID, uID);

           
            
            return View(questionViewModel);
            
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnswer(NewAnswerVIewModel navm)
        {
           //all the fields thare are not submitted in the form are initiailized here before inserted to the DB
            
            navm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            navm.AnsersDateAndTime = DateTime.Now;
            navm.VotesCount = 0;
            if(ModelState.IsValid)
            {
                this.asr.InsertAnswer(navm);
                return RedirectToAction("View", "Question", new { qID = navm.QuestionID });

            }
            else
            {
                ModelState.AddModelError("x", "invliad data");
                //we need to initialize, get the data:

                QuestionViewModel questionViewModel= this.qs.GetQuestionByID(navm.QuestionID, navm.UserID);
                return View("View", questionViewModel);//first argument says which view to go for, and the second is what that view needs (see action result above)
            }
            

        }
    }
}