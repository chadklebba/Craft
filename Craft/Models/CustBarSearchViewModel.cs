﻿using Craft.Models.Craft;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Craft.Models
{
    public class CustBarSearchViewModel
    {
        [Display(Name = "Choose a Beer")]
        public List<SelectListItem> BeerNames { get; set; }
        public string BeerId { get; set; }
        public int BarId { get; set; }
        [Display(Name = "Bar Name")]
        public List<Bar> BeerBars { get; set; }
        [Display(Name = "Address")]
        public List<string> stringAddresses { get; set; }
        [Display(Name = "Legend")]
        public string Legend { get; set; }
        public string CustAddress { get; set; }
        public CustBarSearchViewModel()
        {
            BeerNames = new List<SelectListItem>();
        }
    }
}