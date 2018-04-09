using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HappyGift.Models.ServiceViewModels
{
    public class CreateServiceViewModel
    {
        public long Id {get; set;}

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Service Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public string Price { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        
    }
}
