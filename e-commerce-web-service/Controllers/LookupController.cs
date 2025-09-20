using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;
using e_commerce_web.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_web_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly LookupService lookupService;

        public LookupController(LookupService lookupService)
        {
            this.lookupService = lookupService ?? throw new ArgumentNullException(nameof(lookupService));
        }

        [HttpGet]
        [Route("productCategories")]
        public async Task<IEnumerable<CategoryDto>> GetProductCategoriesAsync()
        {
            return await this.lookupService.GetProductCategoriesAsync();
        }

        [HttpGet]
        [Route("roles")]
        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            return await this.lookupService.GetRolesAsync();
        }

        [HttpGet]
        [Route("brands")]
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            return await this.lookupService.GetBrandsAsync();
        }
    }
}
