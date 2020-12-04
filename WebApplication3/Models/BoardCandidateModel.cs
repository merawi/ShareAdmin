using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class BoardCandidate
    {
        public int ID { get; set; }
        public int CandidateNo { get; set; }
        public string CandidateName { get; set; }
        public bool selected { get; set; }
        public virtual ICollection<Shareholder> ShareholderVotes { get; set; }
    }
}