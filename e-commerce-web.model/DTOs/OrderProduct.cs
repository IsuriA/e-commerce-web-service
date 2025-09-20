namespace e_commerce_web.core.DTOs;

public class OrderItemDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int Quantity { get; set; }

    public ProductDto Product { get; set; } = null!;
}
