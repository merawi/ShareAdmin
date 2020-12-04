using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using WebApplication3.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class AttendanceTransaction
    {
        [Key]
        public int ID { get; set; }

        Shareholder Shareholder { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime RegistrationTimeStamp { get; set; }

    }
}