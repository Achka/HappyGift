using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HappyGift.Models
{
    public class CartServices
    {
        [Key]
        public int CartServiceId { get; set; }
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
