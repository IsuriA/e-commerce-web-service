using e_commerce_web.core.DTOs;
using e_commerce_web.service;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_web_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiryController : ControllerBase
    {
        InquiryService inquiryService;

        public InquiryController(InquiryService inquiryService)
        {
            this.inquiryService = inquiryService ?? throw new ArgumentNullException(nameof(inquiryService));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Add(InquiryDto inquiry)
        {
            try
            {
                await this.inquiryService.AddNewInquiryAsync(inquiry);
            }
            catch (Exception ex)
            {
                throw;
            }

            return Ok();
        }

    }
}
