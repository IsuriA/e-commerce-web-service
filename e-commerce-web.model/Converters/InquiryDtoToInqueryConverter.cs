using AutoMapper;
using e_commerce_web.model.DTOs;
using e_commerce_web.model.Models;

namespace e_commerce_web.model.Converters
{
    public class InquiryDtoToInqueryConverter : ITypeConverter<InquiryDto, Inquiry>
    {
        public Inquiry Convert(InquiryDto source, Inquiry destination, ResolutionContext context)
        {
            if (destination == null) {
                destination = new Inquiry();
            }
            if (source == null) { 
                return destination;
            }

            destination.Message = source.Message;
            destination.UserId = source.UserId;
            destination.StatusId = source.StatusId;

            return destination;
        }
    }
}
