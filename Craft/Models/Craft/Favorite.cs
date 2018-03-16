using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Craft.Models.Craft
{
    public class Favorite
    {
        [Key]
        public int FavoriteId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer{ get; set; }

        [ForeignKey("Beer")]
        public int BeerId { get; set; }
        public virtual Beer Beer{ get; set; }
    }
}