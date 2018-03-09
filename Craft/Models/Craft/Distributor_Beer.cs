using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Craft.Models.Craft
{
    public class Distributor_Beer
    {
        [Key]
        [ForeignKey("distributor")]
        public int DistributorId { get; set; }
        public virtual Distributor distributor { get; set; }

        [ForeignKey("beer")]
        public int BeerId { get; set; }
        public virtual Beer beer { get; set; }
    }
}