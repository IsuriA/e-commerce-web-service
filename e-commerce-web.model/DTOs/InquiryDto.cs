using System.Text.Json.Serialization;

namespace e_commerce_web.core.DTOs
{
    public class InquiryDto
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public int StatusId { get; set; }

        public int UserId { get; set; }
    }
}
