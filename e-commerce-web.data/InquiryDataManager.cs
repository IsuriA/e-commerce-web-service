using e_commerce_web.model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_web.data
{
    public class InquiryDataManager
    {
        private readonly ECommerceDbContext context;

        public InquiryDataManager(ECommerceDbContext dbContext)
        {
            this.context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Inquiry>> GetInquiriesAsync()
        {
            return await this.context.Inquiries.ToListAsync();
        }

        public async Task AddNewInquiryAsync(Inquiry inquiry) {

            await this.context.Inquiries.AddAsync(inquiry);

            await this.context.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await this.context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
};