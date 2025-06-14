using AutoMapper;
using e_commerce_web.data;
using e_commerce_web.model.DTOs;
using e_commerce_web.model.Models;

namespace e_commerce_web.service
{
    public class InquiryService
    {
        private readonly InquiryDataManager inquiryDataManager;
        private readonly LookupDataManager lookupDataManager;
        private readonly IMapper _mapper;
        private const string NEW_INQUIRY_STATUS_CODE = "NEW";

        public InquiryService(InquiryDataManager inquiryDataManager,
            LookupDataManager lookupDataManager,
            IMapper mapper)
        {
            this.inquiryDataManager = inquiryDataManager ?? throw new ArgumentNullException(nameof(inquiryDataManager));
            this.lookupDataManager = lookupDataManager ?? throw new ArgumentNullException(nameof(lookupDataManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Inquiry>> GetAllAsync()
        {
            return await inquiryDataManager.GetInquiriesAsync();
        }

        public async Task AddNewInquiryAsync(InquiryDto inquiry)
        {
            try
            {
                Inquiry inqueryModel = this._mapper.Map<Inquiry>(inquiry);
                int newInquiryStatusId = 1;//this.lookupDataManager.GetInqiryStatuses().FirstOrDefault()?.Id
                   // ?? throw new ApplicationException($"{nameof(InquiryStatus)} NEW is not configured");
                inqueryModel.StatusId = newInquiryStatusId;

                await this.inquiryDataManager.AddNewInquiryAsync(inqueryModel);
            } catch (Exception ex) {
                throw;
            }
        }
    }
}