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
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Amount Owed : $")]
        public int AmountOwed { get; set; }
        public string PickupDay { get; set; }
        public bool OneTimePickup { get; set; }
        public string OneTimePickupDay { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Address { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId {get;set; }
        public ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<Client> EnumerableClient { get; set; }

    }
}