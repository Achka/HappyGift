using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public bool IsAcceptedByAdmin { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string City { get; set; }

        public virtual HappyGiftUser User { get; set; }

        public virtual ICollection<GiftServices> GiftServices { get;set; }
    }
}
