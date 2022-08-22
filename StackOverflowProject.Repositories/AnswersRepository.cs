using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IAnswersRepository
    {
        void InsertAnswer(Answer answer);
        void UpdateAnswer(Answer answer);
        void DeleteAnswer(int answerID);
        /// <summary>
        /// a user will give a vote for an answer
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="uid"></param>
        /// <param name="value"></param>
        void UpdateAnswerVotesCount(int aid, int uid, int value);

        List<Answer> GetAnswersByQuestionID(int questionID);
        List<Answer> GetAnswerByAnswerID(int answerID);
       
    }
    public class AnswersRepository: IAnswersRepository
    {
        StackOverflowDatabaseDBContext db;
        IQuestionsRepository questionRepository;
        IVotesRepository voteRepository;
        

        //constructor - can use dependency injection here later

        public AnswersRepository()
        {
            db = new StackOverflowDatabaseDBContext();
            questionRepository = new QuestionsRepository();
            voteRepository = new VotesRepository();

        }

        public void DeleteAnswer(int answerID)
        {
            
            Answer answerToDelete = db.Answers.Where(x => x.AnswerID == answerID).FirstOrDefault();
          //  int answerVoteCount = answerToDelete.VotesCount; //we are not updating the vote count even though answer is gone

            if(answerToDelete != null)
            {
                questionRepository.UpdateQuestionAnswersCounter(answerToDelete.QuestionID, -1);//one answer removed
                db.Answers.Remove(answerToDelete);
                db.SaveChanges();
                
            }    

                        

        }

        public List<Answer> GetAnswerByAnswerID(int answerID)
        {
            List<Answer> answer = db.Answers.Where(x => x.AnswerID == answerID).ToList();
            return answer;

        }

        public List<Answer> GetAnswersByQuestionID(int questionID)
        {
            List<Answer> answer = db.Answers.Where(x => x.QuestionID == questionID).OrderByDescending(x=>x.AnsersDateAndTime).ToList();
            return answer;
        }

        public void InsertAnswer(Answer answer)
        {
           db.Answers.Add(answer);
           db.SaveChanges();
            //once an answer was inserted, we need to increase counters

            questionRepository.UpdateQuestionAnswersCounter(answer.QuestionID, 1);
        }

        public void UpdateAnswer(Answer answer)
        {
           Answer answerToUpdate = db.Answers.Where(y=>y.AnswerID==answer.AnswerID).FirstOrDefault();

           if (answerToUpdate != null)
           {
                answerToUpdate.AnswerText = answer.AnswerText;
                
                db.SaveChanges();
           }
        }

        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            Answer answerToUpdate = db.Answers.Where(y => y.AnswerID == aid).FirstOrDefault();

            if(answerToUpdate != null)
            {
                answerToUpdate.VotesCount=answerToUpdate.VotesCount+value;
                db.SaveChanges();

                //we have to update the vote count of the question as well as the vote count of the question is the sum of votes for all answers
                questionRepository.UpdateQuestionVoteCounter(answerToUpdate.QuestionID, value);

                //we have the update the votes themselves (as in the vote table we have which user gave whcih vote to which answer
                voteRepository.UpdateVote(answerToUpdate.AnswerID,uid,value);

            }
        }
    }
}
