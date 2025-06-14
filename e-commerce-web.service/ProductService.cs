using AutoMapper;
using e_commerce_web.data;
using e_commerce_web.model;
using e_commerce_web.model.DTOs;
using e_commerce_web.model.Models;
using Microsoft.AspNetCore.Http;

namespace e_commerce_web.service
{
    public class ProductService
    {
        private ProductDataManager productDataManager;
        private readonly LookupDataManager lookupDataManager;
        private readonly AppSettings _appSettings;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ProductService(
            ProductDataManager productDataManager,
            IHttpContextAccessor httpContextAccessor,
            LookupDataManager lookupDataManager,
            IMapper mapper)
        {
            this.productDataManager = productDataManager ?? throw new ArgumentNullException(nameof(productDataManager));
            this.lookupDataManager = lookupDataManager ?? throw new ArgumentNullException(nameof(lookupDataManager));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task AddProductAsync(ProductDto dto)
        {
            try
            {
                Product productModel = this.mapper.Map<Product>(dto);

                UserDto user = (UserDto)this.httpContextAccessor.HttpContext.Items["User"];
                productModel.UserId = user.Id;

                await this.productDataManager.AddNewProductAsync(productModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            IEnumerable<Product> products = await this.productDataManager.GetProductsAsync();

            return products.Select(x =>
            {
                ProductDto dto = this.mapper.Map<ProductDto>(x);
                dto.Category = this.mapper.Map<CategoryDto>(x.Category);
                return dto;
            });
        }
    }
}
