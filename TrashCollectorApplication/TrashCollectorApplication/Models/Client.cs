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
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        [Display(Name = "Suspension Start Date")]
        [DataType(DataType.Date)]
        public DateTime? SuspensionStartDate { get; set; }
        [Display(Name = "Suspension End Date")]
        [DataType(DataType.Date)]
        public DateTime? SuspensionEndDate { get; set; }
        [Display(Name = "One-time Pickup Date")]
        [DataType(DataType.Date)]
        public DateTime? OneTimePickupDate { get; set; }

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