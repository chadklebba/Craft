using Craft.Models.Craft;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Craft.Models
{
    public class FavoritesViewModel
    {
        public virtual Beer Beer { get; set; }
        [Display(Name = "Favorite Beers")]
        public List<Beer> FavoriteBeer { get; set; }
        [Display(Name = "Beers")]
        public List<SelectListItem> AvailableBeerNames { get; set; }
        [Display(Name = "Recommended Beer")]
        public List<Beer> RecommendedBeers { get; set; }
        public int BeerId { get; set; }
        public int CustomerId { get; set; }
        public FavoritesViewModel()
        {
            AvailableBeerNames = new List<SelectListItem>();
            RecommendedBeers = new List<Beer>();
        }
    }
}