﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KlioCarProject.Models
{
    public class Car
    {
        public int CarID { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage ="Please enter a positive price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter a model")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Please enter a type")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Please enter an engine")]
        public string Engine { get; set; }
        [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; }

        [DisplayName("Upload File")]
        [Column(TypeName = "nvarchar(100)")]
        public string ImageName { get; set; }

        [DisplayName("Upload File")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
