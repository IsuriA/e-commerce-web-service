using e_commerce_web.model.DTOs;
using e_commerce_web.model.Models;
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
        [Authorize]
        public IEnumerable<CategoryDto> GetProductCategories()
        {
            return this.lookupService.GetProductCategories();
        }

        [HttpGet]
        [Route("roles")]
        public IEnumerable<RoleDto> GetRoles()
        {
            return this.lookupService.GetRoles();
        }

        [HttpGet]
        [Route("brands")]
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            return await this.lookupService.GetBrandsAsync();
        }
    }
}
