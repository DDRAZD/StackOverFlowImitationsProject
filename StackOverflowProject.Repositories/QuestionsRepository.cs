using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;


namespace StackOverflowProject.Repositories
{
    public interface IQuestionsRepository
    {
        void InsertQuestion(Question question);
        void UpdateQuestionDetails(Question question);
        /// <summary>
        /// this will update the counter with the value of the vote - it will be 1 or -1
        /// </summary>        
        void UpdateQuestionVoteCounter(int qid, int value);
        /// <summary>
        /// if new answer is added it will be +1 if deleted it will be -1
        /// </summary>
        /// <param name="qid"></param>
        /// <param name="value"></param>
        void UpdateQuestionAnswersCounter(int qid, int value);

        /// <summary>
        /// this will only increment
        /// </summary>
        /// <param name="qid"></param>
        void UpdateQuestionViewsCounter(int qid);
        void DeleteQuestion(int qid);
        List<Question> GetAllQuestions();
        List<Question> GetQuestionByQuestionID(int qid);
    }
    public class QuestionsRepository : IQuestionsRepository
    {
        StackOverflowDatabaseDBContext db;

        //constructor (can use later depenancy injection)

        public QuestionsRepository()
        {
            db = new StackOverflowDatabaseDBContext();
        }
        public void DeleteQuestion(int qid)
        {
            Question questionToDelete = db.Questions.Where(y=>y.QuestionID == qid).FirstOrDefault();
            if (questionToDelete != null)
            {
                db.Questions.Remove(questionToDelete);
                db.SaveChanges();
            }
        }

        public List<Question> GetAllQuestions()
        {
            return db.Questions.OrderByDescending(y=>y.QuestionDateAndTime).ToList();
        }

        public List<Question> GetQuestionByQuestionID(int qid)
        {
            List<Question> questions= db.Questions.Where(y=>y.QuestionID ==qid).ToList();

           return questions;
        }

        public void InsertQuestion(Question question)
        {
            db.Questions.Add(question);
            db.SaveChanges();
        }

        public void UpdateQuestionAnswersCounter(int qid, int value)
        {
            Question q = db.Questions.Where(y => y.QuestionID == qid).FirstOrDefault();
            if (q != null)
            {
                q.AnswersCount = q.AnswersCount + value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionDetails(Question question)
        {
            Question q = db.Questions.Where(y => y.QuestionID == question.QuestionID).FirstOrDefault();
            if (q != null)
            {
                q.QuestionName =question.QuestionName;
                q.QuestionDateAndTime = question.QuestionDateAndTime;
                q.CategoryID = question.CategoryID;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCounter(int qid)
        {
            Question q = db.Questions.Where(y => y.QuestionID == qid).FirstOrDefault();
            if (q != null)
            {
                q.ViewsCounter++; ;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionVoteCounter(int qid, int value)
        {
            Question q = db.Questions.Where(y => y.QuestionID == qid).FirstOrDefault();
            if(q != null)
            {
                q.VotesCount = q.VotesCount + value;
                db.SaveChanges();
            }
        }
    }
}
