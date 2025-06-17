using AutoMapper;
using e_commerce_web.data;
using e_commerce_web.model;
using e_commerce_web.model.DTOs;
using e_commerce_web.model.Models;
using Microsoft.AspNetCore.Http;

namespace e_commerce_web.service
{
    public class LookupService
    {
        private readonly LookupDataManager lookupDataManager;
        private readonly UserDataManager userDataManager;
        private readonly AppSettings _appSettings;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LookupService(
            LookupDataManager lookupDataManager,
            UserDataManager userDataManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            this.lookupDataManager = lookupDataManager ?? throw new ArgumentNullException(nameof(lookupDataManager));
            this.userDataManager = userDataManager ?? throw new ArgumentNullException(nameof(userDataManager));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public IEnumerable<CategoryDto> GetProductCategories()
        {
            IEnumerable<Category> categories = this.lookupDataManager.GetProductCategories();
            return categories.Select(c => this.mapper.Map<CategoryDto>(c));
        }

        public IEnumerable<RoleDto> GetRoles()
        {
            IEnumerable<Role> roles = this.lookupDataManager.GetRoles();

            return roles.Select(c => this.mapper.Map<RoleDto>(c));
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            IEnumerable<Brand> brands = await this.lookupDataManager.GetBrandsAsync();

            return brands.Select(c => this.mapper.Map<BrandDto>(c));
        }
    }
}
