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
    public class QuestionsController : ApiController
    {
        
        IAnswersService ans;

        //constructor

        public QuestionsController(IAnswersService ans)
        {
           
            this.ans = ans;
        }

        public void Post(int AnswerID, int UserID, int Value)
        {
            this.ans.UpdateAnswerVotesCount(AnswerID, UserID, Value);
        }
    }
}
