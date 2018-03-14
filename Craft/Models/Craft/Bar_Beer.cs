using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Craft.Models.Craft
{
    public class Bar_Beer
    {
        [Key]
        public int Bar_BeerId { get; set; }

        [ForeignKey("Beer")]
        public int BeerId { get; set; }
        public virtual Beer Beer { get; set; }

        [ForeignKey("Bar")]
        public int BarId { get; set; }
        public virtual Bar Bar { get; set; }
    }
}