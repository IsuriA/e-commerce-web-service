using e_commerce_web.model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace e_commerce_web.data
{
    public class LookupDataManager
    {
        private readonly ECommerceDbContext context;
        private readonly IMemoryCache memoryCache;

        private enum LookupCacheKeys
        {
            InquiryStatuses,
            ProductCategories,
            UserRoles,
            Brands
        }

        public LookupDataManager(ECommerceDbContext dbContext,
            IMemoryCache memoryCache)
        {
            this.context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<IEnumerable<InquiryStatus>> GetInqiryStatusesAsync()
        {
            if (!memoryCache.TryGetValue(LookupCacheKeys.InquiryStatuses, out IEnumerable<InquiryStatus> inquiryStatuses))
            {
                inquiryStatuses = await this.context.InquiryStatuses.ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                memoryCache.Set(LookupCacheKeys.InquiryStatuses, inquiryStatuses, cacheEntryOptions);
            }

            return inquiryStatuses ?? new List<InquiryStatus>();
        }

        public async Task<IEnumerable<Category>> GetProductCategoriesAsync()
        {
            if (!memoryCache.TryGetValue(LookupCacheKeys.ProductCategories, out IEnumerable<Category> categories))
            {
                categories = await this.context.Categories.ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                memoryCache.Set(LookupCacheKeys.ProductCategories, categories, cacheEntryOptions);
            }

            return categories ?? new List<Category>();
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            if (!memoryCache.TryGetValue(LookupCacheKeys.UserRoles, out IEnumerable<Role> roles))
            {
                roles = await this.context.Roles.ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                memoryCache.Set(LookupCacheKeys.UserRoles, roles, cacheEntryOptions);
            }

            return roles ?? new List<Role>();
        }

        public async Task<IEnumerable<Brand>> GetBrandsAsync()
        {
            if (!memoryCache.TryGetValue(LookupCacheKeys.Brands, out IEnumerable<Brand> brands))
            {
                brands = await this.context.Brands.ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                memoryCache.Set(LookupCacheKeys.Brands, brands, cacheEntryOptions);
            }

            return brands ?? new List<Brand>();
        }
    }
}