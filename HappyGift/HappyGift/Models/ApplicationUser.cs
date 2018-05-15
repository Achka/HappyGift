using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HappyGift.Models
{
    // Add profile data for application users by adding properties to the HappyGiftUser class
    public class HappyGiftUser : IdentityUser
    {
        public virtual ICollection<Gift> Gifts { get; set; }

        public string Place { get; set; }
        [DefaultValue(1900)]
        public int? YearOfBirth { get; set; }
    }
}
