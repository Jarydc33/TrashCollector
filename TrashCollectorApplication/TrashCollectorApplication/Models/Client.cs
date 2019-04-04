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
        public bool AccountSuspended { get; set; }
        public bool OneTimePickup { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string SuspensionStart { get; set; }
        public string SuspensionEnd { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId {get;set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("PickupDay")]
        [Display(Name = "Pickup Day")]
        public int? PickupDayId { get; set; }
        public int? OneTimePickupDayId { get; set; }
        public PickupDay PickupDay { get; set; }

        public IEnumerable<PickupDay> PickupDays { get; set; }

    }
}