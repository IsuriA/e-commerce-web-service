using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            UserRoles
        }

        public LookupDataManager(ECommerceDbContext dbContext,
            IMemoryCache memoryCache)
        {
            this.context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public IEnumerable<InquiryStatus> GetInqiryStatuses()
        {
            if (!memoryCache.TryGetValue(LookupCacheKeys.InquiryStatuses, out IEnumerable<InquiryStatus> inquiryStatuses))
            {
                inquiryStatuses = this.context.InquiryStatuses;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                memoryCache.Set(LookupCacheKeys.InquiryStatuses, inquiryStatuses, cacheEntryOptions);
            }

            return inquiryStatuses ?? new List<InquiryStatus>();
        }

        public IEnumerable<Category> GetProductCategories()
        {
            if (!memoryCache.TryGetValue(LookupCacheKeys.InquiryStatuses, out IEnumerable<Category> categories))
            {
                categories = this.context.Categories;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                memoryCache.Set(LookupCacheKeys.ProductCategories, categories, cacheEntryOptions);
            }

            return categories ?? new List<Category>();
        }

        public IEnumerable<Role> GetRoles()
        {
            if (!memoryCache.TryGetValue(LookupCacheKeys.InquiryStatuses, out IEnumerable<Role> roles))
            {
                roles = this.context.Roles;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                memoryCache.Set(LookupCacheKeys.ProductCategories, roles, cacheEntryOptions);
            }

            return roles ?? new List<Role>();
        }
    }
}