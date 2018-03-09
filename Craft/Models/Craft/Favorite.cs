using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Craft.Models.Craft
{
    public class Favorite
    {
        [ForeignKey("CustomerId")]
        public string CustomerId { get; set; }
        public virtual Customer Customer{ get; set; }

        [ForeignKey("Beer")]
        public string BeerId { get; set; }
        public virtual Beer Beer{ get; set; }
    }
}