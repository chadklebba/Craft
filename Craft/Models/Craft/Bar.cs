using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Craft.Models.Craft
{
    public class Bar
    {
        [Key]
        public int BarId { get; set; }
        [Display(Name = "Bar Name")]
        public string BarName { get; set; }
        public string Address { get; set; }
        public List<Beer> ListOfBeer { get; set; }
        [Display(Name = "Legend")]
        public string Legend { get; set; }
        public string CustAddress { get; set; }
    }
}