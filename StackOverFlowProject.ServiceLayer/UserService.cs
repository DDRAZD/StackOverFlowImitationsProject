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
    public interface IUserservice
    {
      //  IUsersRepository UsersRepository { get; }

        int InserUser(RegisterViewModel uvm);
        void UpdateUserDetail(EditUserDetailsViewModel uvm);
        void UpdateUserPassowrd(EditUserPasswordViewModel uvm);

        void DeleteUser(int uid);
        List<UserViewModel> GetUsers();

        UserViewModel GetUserByEmailandPassword(string Email, string Password);
        UserViewModel GetUserByEmail(string Email);
        UserViewModel GetUserByUserID(int UserID);



    }
    public class UserService : IUserservice
    {
        IUsersRepository ur;

        //constructor
        public UserService()
        {
            ur = new UsersRepository();
        }

        public void DeleteUser(int uid)
        {
           ur.DeleteUser(uid);
        }

        public UserViewModel GetUserByEmail(string Email)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetUserByEmailandPassword(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetUserByUserID(int UserID)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> GetUsers()
        {
            throw new NotImplementedException();
        }

        public int InserUser(RegisterViewModel uvm)
        {
            //configuring the automapper so to transition from a view model to domain model (registerviewmodel to user)
            var config = new MapperConfiguration(cfg => { 
                cfg.CreateMap<RegisterViewModel, User>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<RegisterViewModel, User>(uvm); //left is the source type, right is the destination type, like massive casting
            u.PasswordHash = SHA256HashGenerator.GenerateHash(uvm.Password); //creating the password hash
            ur.InsertUser(u);
            int uid = ur.GetLatestUserID();
            return uid;
               
        }

        public void UpdateUserDetail(EditUserDetailsViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EditUserDetailsViewModel, User>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();           
            User u = mapper.Map<EditUserDetailsViewModel, User>(uvm); //left is the source type, right is the destination type, like massive casting
            ur.UpdateUserDetails(u);
        }

        public void UpdateUserPassowrd(EditUserPasswordViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EditUserPasswordViewModel, User>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();           
            User u = mapper.Map<EditUserPasswordViewModel, User>(uvm); //left is the source type, right is the destination type, like massive casting
            u.PasswordHash = SHA256HashGenerator.GenerateHash(uvm.Password); 
            ur.UpdateUserPassword(u);
        }
    }
}
