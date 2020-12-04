using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class ShareholdersAdminDB : DbContext
    {
        public ShareholdersAdminDB()
            : base("name=DefaultConnection")
        {
        }
        public DbSet<Shareholder> Shareholders { get; set; }
        public DbSet<BoardCandidate> BoardCandidates { get; set; }

        public System.Data.Entity.DbSet<WebApplication3.Models.CandidateViewModel> CandidateViewModels { get; set; }

    }
}