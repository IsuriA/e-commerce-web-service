using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_commerce_web.model.Models;
using Microsoft.AspNetCore.Http;

namespace e_commerce_web.model.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        //public IFormFile ImageFile { get; set; }

        public string ImageUrl { get; set; }

        public CategoryDto Category { get; set; }

        public BrandDto Brand { get; set; }

    }
}
