using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HappyGift.Models
{
    public class Gift
    {
        public Gift()
        {

        }
        [Key]
        public long Id { get; set; }

        public string UserId { get; set; }

        public long ServiceId { get; set; }

        public bool IsAcceptedByAdmin { get; set; }

        public virtual HappyGiftUser User { get; set; }

        public virtual ICollection<GiftServices> GiftServices { get;set; }
    }
}
