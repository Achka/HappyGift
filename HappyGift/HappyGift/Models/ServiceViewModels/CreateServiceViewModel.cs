﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HappyGift.Models.ServiceViewModels
{
    public class CreateServiceViewModel
    {
        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.Text)]
        public string Id {get; set;}
       
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Service Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }


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

        [Required]
        [Display(Name = "Duration (in minutes)")]
        public int Duration { get; set; }
        
    }
}
