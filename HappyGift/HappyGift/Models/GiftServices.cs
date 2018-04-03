using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HappyGift.Models
{
    public class GiftServices
    {
        [Key]
        public int GiftServiceId { get; set; }
        public int GiftId { get; set; }
        public virtual Gift Gift{get; set;}

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
