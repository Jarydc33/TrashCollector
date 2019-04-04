using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollectorApplication.Models
{
    public class Client
    {
        [Key]
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AmountOwed { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId {get;set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}