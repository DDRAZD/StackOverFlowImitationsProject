﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowProject.DomainModels
{
    public class Answer
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public DateTime AnsersDateAndTime { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public int VotesCount { get; set; }

        //connecting it to the other tables at run time

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("QuestionID")]
        public virtual Question Question { get; set; }

        public virtual List<Vote> Votes { get; set; }
    }
}
