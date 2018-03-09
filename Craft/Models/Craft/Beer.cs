using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Craft.Models.Craft
{
    public class Beer
    {
        [Key]
        public int BeerId { get; set; }
        [Display(Name = "Beer Name")]
        public string BeerName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public double ABV { get; set; }

    }
}