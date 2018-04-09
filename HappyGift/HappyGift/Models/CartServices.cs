using System.ComponentModel.DataAnnotations;

namespace HappyGift.Models
{
    public class CartServices
    {
        [Key]
        public int CartServiceId { get; set; }
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public long ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
