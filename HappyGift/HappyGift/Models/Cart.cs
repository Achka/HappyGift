using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HappyGift.Models
{
    public class Cart
    {
        public Cart()
        {
            CartServices = new HashSet<CartServices>();
        }
        [Key]
        public int CartId { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<CartServices> CartServices { get; set; }
    }
}
