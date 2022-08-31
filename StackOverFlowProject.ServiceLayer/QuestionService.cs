using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;
using StackOverflowProject.ViewModels;
using StackOverflowProject.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverflowProject.ServiceLayer
{

    public interface IQuestionService
    {

        void InsertQuestion(NewAnswerVIewModel qvm);
        void UpdateQuestionDetails(EditAnswerViewModel qvm);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAsnwersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid, int value);  
        void DeleteQuestion(int qid);
        List<QuestionViewModel> GetAllQuestions();
        QuestionViewModel GetQuestionByID(int qid, int userid);

    }
    public class QuestionService: IQuestionService
    {
        IQuestionsRepository qr;
        //constrcutre
        public QuestionService()
        {
            qr = new QuestionsRepository();
        }

        public void DeleteQuestion(int qid)
        {
          qr.DeleteQuestion(qid);
        }

        public List<QuestionViewModel> GetAllQuestions()
        {
            List<Question> questions = qr.GetAllQuestions();

            List<QuestionViewModel> qvms = null;

            if (questions != null)
            {
                //mapping to move it to DomainModel
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Question, QuestionViewModel>();
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.CreateMap<Vote, VoteViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                qvms = mapper.Map<List<Question>,List<QuestionViewModel>>(questions); //left is the source type, right is the destination type, like massive casting
            }
            return qvms;
        }

        public QuestionViewModel GetQuestionByID(int qid, int userid=0)
        {
            Question question = qr.GetQuestionByQuestionID(qid).FirstOrDefault();
            QuestionViewModel qvm = null;

            if (question != null)
            {//mapping to move it to DomainModel
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Question, QuestionViewModel>();
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.CreateMap<Vote, VoteViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                qvm = mapper.Map<Question, QuestionViewModel>(question); //left is the source type, right is the destination type, like massive casting
            }

            //loading all the answers that are related
            //because in teh domain model we defiend the answers as "virtual" in questions, all the answers will be loaded at run time
            //here we are simply reading them
            //also need to check the userID for the answers
            //WE WILL RECALCULATE the vote coutner for every answer when we present the question - as votes are stored in the DB
            //(data layer) and we access it via the repository, we can manipulate the value in the service layer (in the AnswerViewModel)
            //without worrying about losing data. THis is simply a prep to send it up to the presentation layer (the Controller and view)
            foreach(var item in qvm.Answers)
            {
                item.CurrentUserVoteType = 0; //we start in initializing the AnswersViewModel with neutral, not an up vote, not a down vote
                VoteViewModel vote = item.Votes.Where(y=>y.UserID== userid).FirstOrDefault();//this is done post mapping, we have in the AnserViewModel the information about the atual votes as they are virtual foregin key in the answer domainmodel
                if(vote!=null)
                {
                    item.CurrentUserVoteType = vote.VoteValue;//can be +1 or -1
                }
            }
            //after the loop ended, our qvm that is connected to AnswersViewModel that is connected to VoteViewModel, will be updated with
            //the right vote numbers; so when the question is pulled, the vote counter will be updated for presentation and we will
            //know what the user voted for these answers

            return qvm;
        }

        public void InsertQuestion(NewAnswerVIewModel qvm)
        {
            //mapping to move it to DomainModel
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<NewAnswerVIewModel, Question>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();           
            Question questionToInsert = mapper.Map<NewAnswerVIewModel, Question>(qvm); //left is the source type, right is the destination type, like massive casting

            qr.InsertQuestion(questionToInsert);
        }

        public void UpdateQuestionAsnwersCount(int qid, int value)
        {
            qr.UpdateQuestionAnswersCounter(qid, value);
        }

        public void UpdateQuestionDetails(EditAnswerViewModel qvm)
        {
            //mapping to move it to DomainModel
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EditAnswerViewModel, Question>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<EditAnswerViewModel, Question>(qvm); //left is the source type, right is the destination type, like massive casting


            qr.UpdateQuestionDetails(q);
        }

        public void UpdateQuestionViewsCount(int qid, int value)
        {
            qr.UpdateQuestionViewsCounter(qid, value);
        }

        public void UpdateQuestionVotesCount(int qid, int value)
        {
            qr.UpdateQuestionVoteCounter(qid, value);
        }
    }
}
