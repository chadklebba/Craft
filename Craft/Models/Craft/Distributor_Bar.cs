using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Craft.Models.Craft
{
    public class Distributor_Bar
    {
        [Key]
        public int Distributor_BarId { get; set; }

        [ForeignKey("distributor")]
        public int DistributorId { get; set; }
        public virtual Distributor distributor { get; set; }

        [ForeignKey("bar")]
        public int BarId { get; set; }
        public virtual Bar bar { get; set; }
    }
}