using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Shareholder
    {
        [Key]
        public int ID { get; set; }

        [Display(Name="Shareholder ID")]
        public string ShareholderID { get; set; }
        [Display(Name="Sharenolder Name")]
        public string ShareholderName { get; set; }
        [Display(Name = "የባለድርሻ ስም")]
        public string ShareholderNameAmh { get; set; }
        [Display(Name="Subscribed Shares")]
        public int NSharesSubscribed { get; set; }
        [Display(Name="Paid Shares")]
        public float NSharesPaid { get; set; }
        [Display(Name="Share Serials")]
        public string VoteRegisterer { get; set; }
        public string AttendanceRegisterer { get; set; }
        public bool Attended { get; set; }
        public bool Voted { get; set; }
       // public virtual ICollection<BoardCandidate> ElectedCandidates {get; set;}
        public virtual List<String> ElectedCandidateIDs { get; set; }

        public Shareholder()
        {
            ElectedCandidateIDs = new List<String>();
        }

    }
}