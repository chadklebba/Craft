using Craft.Models.Craft;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Craft.Models
{
    public class AddBeerViewModel
    {
        public virtual Beer Beer { get; set; }
        [Display(Name = "Available Beers")]
        public List<SelectListItem> AvailableBeerNames { get; set; }
        [Display(Name = "This Bar's Beer List")]
        public List<Beer> AddedBeers { get; set; }
        public int BarId { get; set; }
        public int BeerId { get; set; }

        public AddBeerViewModel()
        {
            AvailableBeerNames = new List<SelectListItem>();
            AddedBeers = new List<Beer>();

        }

    }
}