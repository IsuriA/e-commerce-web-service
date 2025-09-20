using e_commerce_web.core.Models;
using e_commerce_web.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using e_commerce_web.core.DTOs;

namespace e_commerce_web_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService prodcutService;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductController(ProductService productService, IWebHostEnvironment env)
        {
            this.prodcutService = productService ?? throw new ArgumentNullException(nameof(productService));
            this.hostEnvironment = env;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Add([FromForm] ProductDto productDto)
        {
            try
            {
                string fileName = string.Empty;
                // Create folder path
                var productsFolder = Path.Combine(hostEnvironment.ContentRootPath, "Uploads", "Products");
                if (!Directory.Exists(productsFolder))
                {
                    Directory.CreateDirectory(productsFolder);
                }

                foreach (IFormFile file in productDto.ImageFiles)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(productsFolder, fileName);
                    // Save file to server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                productDto.ImageUrl = "Products/" + fileName;
                await this.prodcutService.AddProductAsync(productDto);

                return Ok(new { message = "Product added successfully." });
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetUsers()
        {
            return await this.prodcutService.GetAll();
        }

        [HttpGet("brand/{brandId}")]
        public async Task<IActionResult> GetProductsByBrand(int brandId)
        {
            var products = await this.prodcutService.GetProductsByBrandAsync(brandId);

            return Ok(products);
        }

        [HttpGet("category/{categoryid}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await this.prodcutService.GetProductsByCategoryAsync(categoryId);

            return Ok(products);

        }
    }
}
