using AutoMapper;
using e_commerce_web.data;
using e_commerce_web.model;
using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;
using Microsoft.AspNetCore.Http;

namespace e_commerce_web.service
{
    public class CartService
    {
        private CartDataManager cartDataManager;
        private readonly LookupDataManager lookupDataManager;
        private readonly AppSettings _appSettings;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CartService(
            CartDataManager cartDataManager,
            IHttpContextAccessor httpContextAccessor,
            LookupDataManager lookupDataManager,
            IMapper mapper)
        {
            this.cartDataManager = cartDataManager ?? throw new ArgumentNullException(nameof(cartDataManager));
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

                await this.cartDataManager.AddNewProductAsync(productModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            IEnumerable<Product> products = await this.cartDataManager.GetProductsAsync();

            return products.Select(x =>
            {
                ProductDto dto = this.mapper.Map<ProductDto>(x);
                dto.Category = this.mapper.Map<CategoryDto>(x.Category);
                return dto;
            });
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByBrandAsync(int brandId)
        {
            IEnumerable<Product> products = await this.cartDataManager.GetProductsByBrandAsync(brandId);

            return products.Select(x =>
            {
                ProductDto dto = this.mapper.Map<ProductDto>(x);
                dto.Category = this.mapper.Map<CategoryDto>(x.Category);
                return dto;
            });
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            IEnumerable<Product> products = await this.cartDataManager.GetProductsByCategoryAsync(categoryId);

            return products.Select(x =>
            {
                ProductDto dto = this.mapper.Map<ProductDto>(x);
                dto.Category = this.mapper.Map<CategoryDto>(x.Category);
                return dto;
            });
        }

    }
}
