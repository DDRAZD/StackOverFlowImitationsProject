using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowProject.DomainModels
{
    public class StackOverflowDatabaseDBContext:DbContext
    {

        //constructor
        public StackOverflowDatabaseDBContext():base("StackOverflowDatabaseDBContext")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }
}
