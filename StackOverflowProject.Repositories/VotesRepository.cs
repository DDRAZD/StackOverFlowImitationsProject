using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IVotesRepository
    {
        void UpdateVote(int answerID, int userID, int value);
    }
    public class VotesRepository : IVotesRepository
    {
        StackOverflowDatabaseDBContext dbContext;

        //constructor - can use dependancy injection later
        public VotesRepository()
        {
            dbContext = new StackOverflowDatabaseDBContext();
        }

        public void UpdateVote(int answerID, int userID, int value)
        {
            Vote voteToUpdate = dbContext.Votes.Where(y=>y.AnswerID == answerID && y.UserID == userID).FirstOrDefault();
            int updateValue;
            //we are doing the votes like NPS, detractors and supporters
            if (value > 0) updateValue = 1;
            else if (value < 0) updateValue = -1;
            else updateValue = 0;
            if (voteToUpdate != null)
            {
                
                voteToUpdate.VoteValue=voteToUpdate.VoteValue+updateValue;
                
            }
            else//if the record does not exist, we need to create it
            {
                Vote newVote = new Vote() { AnswerID = answerID, UserID = userID, VoteValue = updateValue };
                dbContext.Votes.Add(newVote);
               
            }
            dbContext.SaveChanges();
        }
    }
}
