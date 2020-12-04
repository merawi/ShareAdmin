using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class CandidateViewModel
    {
        [Key]
        public int ID { get; set; }
        public BoardCandidate CandidateVotedFor { get; set; }
        public bool selected { get; set; }
    }
}