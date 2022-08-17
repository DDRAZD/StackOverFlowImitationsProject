using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowProject.DomainModels
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionID { get; set; }

        public string QuestionName { get; set; }
        public DateTime QuestionDateAndTime { get; set; }
        
        public int UserID { get; set; }
        
       
        public int CategoryID { get; set; }
      
        public int VotesCount { get; set; }
        public int AnswersCount { get; set; }
        public int ViewsCounter { get; set; }

        //adding the connection to the other tables - the foregin key is attached to the IDs specified above
        //whenever data is retrieved, automatically, the below data elements will be binded to it from the User and Category tables
        //"virtual" means assign the data only at runtime

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public virtual List<Answer> Answers { get; set; }
    }
}
         
