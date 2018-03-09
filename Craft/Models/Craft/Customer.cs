using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Craft.Models.Craft
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Street")]
        public string Street { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public List<Beer> FavoriteBeer { get; set; }

       

        [Display(Name = "Address")]
        public string FullAddress
        {
            get
            {
                return Street + " " + City + ", " + State + " " + ZipCode;
            }
        }
    }
}