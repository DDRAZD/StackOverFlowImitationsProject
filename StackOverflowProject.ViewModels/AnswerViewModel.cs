using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowProject.ViewModels
{
    public class AnswerViewModel
    {

        public int AnswerID { get; set; }
        [Required]
        public string AnswerText { get; set; }
        [Required]
        public DateTime AnsersDateAndTime { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int QuestionID { get; set; }
        [Required]
        public int VotesCount { get; set; }

        public virtual UserViewModel User { get; set; }
        public virtual List<VoteViewModel> Votes { get; set; }

        public int CurrentUserVoteType { get; set; }
    }
}
