using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollectorApplication.Models
{
    public class PickupDay
    {
        [Key]
        public int id { get; set; }
        public string GarbagePickupDay { get; set; }
    }
}