using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HappyGift.Models
{
    public class Service
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
        
        public string Description { get; set; }

        public virtual ICollection<Gift> Gifts { get; set; }
    }
}
