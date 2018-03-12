using Craft.Models.Craft;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Craft.Models
{
    public class AddBarViewModel
    {
        public virtual Bar Bar { get; set; }
        [Display(Name = "Available Bars")]
        public List<SelectListItem> AvailableBarNames { get; set; }
        public List<Bar> AddedBars { get; set; }
        
        public int BarID { get; set; }

        public AddBarViewModel()
        {
            AvailableBarNames = new List<SelectListItem>();
            AddedBars = new List<Bar>();
            
        }

    }
}