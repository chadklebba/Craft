using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Craft.Models.Craft
{
    public class Bar_Beer
    {
        [ForeignKey("Beer")]
        public string BeerId { get; set; }
        public virtual Beer Beer { get; set; }

        [ForeignKey("Bar")]
        public string BarId { get; set; }
        public virtual Bar Bar { get; set; }
    }
}