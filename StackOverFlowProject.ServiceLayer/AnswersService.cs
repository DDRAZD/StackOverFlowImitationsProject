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

namespace StackOverFlowProject.ServiceLayer
{
    public interface IAnswersService
    {
        void InsertAnswer(NewAnswerVIewModel avm);
        void UpdateAnswer(EditAnswerViewModel avm);
        void DeleteAnswer(int answerID);
        
        void UpdateAnswerVotesCount(int aid, int uid, int value);

        List<AnswerViewModel> GetAnswersByQuestionID(int questionID);
        AnswerViewModel GetAnswerByAnswerID(int answerID);
    }
    public class AnswersService: IAnswersService
    {
        IAnswersRepository ar;

        //constructor
        public AnswersService()
        {
            ar = new AnswersRepository();
        }

        public void DeleteAnswer(int answerID)
        {
            ar.DeleteAnswer(answerID);
        }

        public AnswerViewModel GetAnswerByAnswerID(int answerID)
        {
           Answer answer = ar.GetAnswerByAnswerID(answerID).FirstOrDefault();
           AnswerViewModel avm = null;

            if (answer != null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                avm = mapper.Map<Answer, AnswerViewModel>(answer); //left is the source type, right is the destination type, like massive casting
            }
            return avm;
        }

        public List<AnswerViewModel> GetAnswersByQuestionID(int questionID)
        {
            List<Answer> answers = ar.GetAnswersByQuestionID(questionID);
            List<AnswerViewModel> answerList = null;
            if(answers != null)
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                answerList = mapper.Map<List<Answer>, List<AnswerViewModel>>(answers); //left is the source type, right is the destination type, like massive casting
            }
            return answerList;

            
        }

        public void InsertAnswer(NewAnswerVIewModel avm)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<NewAnswerVIewModel, Answer>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Answer answer = mapper.Map<NewAnswerVIewModel, Answer>(avm); //left is the source type, right is the destination type, like massive casting
            ar.InsertAnswer(answer);
        }

        public void UpdateAnswer(EditAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EditAnswerViewModel, Answer>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Answer answer = mapper.Map<EditAnswerViewModel, Answer>(avm); //left is the source type, right is the destination type, like massive casting
            ar.UpdateAnswer(answer);
        }

        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            ar.UpdateAnswerVotesCount(aid, uid, value);
        }
    }
}
