using e_commerce_web.core.DTOs;

namespace e_commerce_web.core.Models;

public partial class OrderDto
{
    public int Id { get; set; }

    public OrderStatusDto Status { get; set; }

    public decimal Amount { get; set; }

    public UserDto User { get; set; }

    public DateTime CreatedAt { get; set; }

    public UserDto DeliveryPerson { get; set; }

    public List<OrderItemDto> Items { get; set; }
}
