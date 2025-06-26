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

        public List<IFormFile> ImageFiles { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public CategoryDto Category { get; set; }

        public BrandDto Brand { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
