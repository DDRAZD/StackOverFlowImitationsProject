using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StackOverflowProject.DomainModels
{
    public class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoteID { get; set; }
        public int UserID { get; set; }
        public int AnswerID { get; set; }
        public int VoteValue { get; set; }

     /*   //connecting this table to the other tables to be filled at run time using the foreign keys

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        [ForeignKey("AnswerID")]
        public virtual Answer Answer { get; set; }*/

        
    }
}
