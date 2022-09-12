using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowProject.ViewModels
{
    /// <summary>
    /// this view model is for the users to be able to view the question - these are the data elements we wish to show
    /// </summary>
    public class QuestionViewModel
    {
        [Required]
        public int QuestionID { get; set; }
        [Required]
        public string QuestionName { get; set; }
        [Required]
        public DateTime QuestionDateAndTime { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int VotesCount { get; set; }
        [Required]
        public int AnswersCount { get; set; }
        [Required]
        public int ViewsCounter { get; set; }

        public virtual UserViewModel User { get; set; }
        public virtual CategoryViewModel Category { get; set; }
        public virtual List<AnswerViewModel> Answers { get; set; }
    }
}
