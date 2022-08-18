using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{

    public interface IUsersRepository
    {
        void InsertUser(User user); 
        void UpdateUserDetails(User user);
        void UpdateUserPassword(User user);
        void DeleteUser(int uID);
        List<User> GetAllUsers();
        List<User> GetUsersByEmailAndPassword(string email, string passwordHash);
        List<User> GetUserByEmail(string email);
        List<User> GetUserByUserID(int userId);
        int GetLatestUserID();
    }
    public class UsersRepository : IUsersRepository
    {
        StackOverflowDatabaseDBContext db;

        //constructor

        public UsersRepository()
        {
            db = new StackOverflowDatabaseDBContext();//can be swapped later with dependancy injection
        }
        public void DeleteUser(int uID)
        {
            User userToDelete = db.Users.Where(u => u.UserID == uID).FirstOrDefault();
            if(userToDelete != null)
            {
                db.Users.Remove(userToDelete);

                db.SaveChanges();
            }
            
        }

        public List<User> GetAllUsers()
        {
            //returning only users, not admins
            List<User> listOfUsers = db.Users.Where(y=>y.IsAdmin==false).OrderBy(x=>x.Name).ToList();
            return listOfUsers;
                       
        }

        public int GetLatestUserID()
        {
          int uID = db.Users.Select(y => y.UserID).Max();
            return uID;
        }

        public List<User> GetUserByEmail(string email)
        {
            List<User> userFound = db.Users.Where(y => y.Email == email).ToList();
            return userFound;
        }

        public List<User> GetUserByUserID(int userId)
        {
            List<User> userFound = db.Users.Where(y => y.UserID == userId).ToList();
            return userFound;
        }

        public List<User> GetUsersByEmailAndPassword(string email, string passwordHash)
        {
           List<User> userFound = db.Users.Where(y=>y.Email==email && y.PasswordHash== passwordHash).ToList();
          return userFound;
        }

        public void InsertUser(User user)
        {
           db.Users.Add(user);
           db.SaveChanges();
        }

        public void UpdateUserDetails(User user)
        {
            User userToUpdate = db.Users.FirstOrDefault(x => x.UserID == user.UserID);
            if(userToUpdate != null)
            {
               // userToUpdate.Email = user.Email;
                userToUpdate.Name = user.Name;
                //   userToUpdate.PasswordHash = user.PasswordHash;
                userToUpdate.Mobile = user.Mobile;
               // userToUpdate.IsAdmin = user.IsAdmin;
                db.SaveChanges();
            }
            

        }

        public void UpdateUserPassword(User user)
        {
            User userToUpdate = db.Users.FirstOrDefault(x => x.UserID == user.UserID);
            if (userToUpdate != null)
            {                
                userToUpdate.PasswordHash = user.PasswordHash;                
                db.SaveChanges();
            }
        }
    }
}
