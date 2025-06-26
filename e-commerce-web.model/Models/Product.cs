using System;
using System.Collections.Generic;

namespace e_commerce_web.model.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public int BrandId { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }

    public int? SupplierId { get; set; }

    public int? PromotionId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual Promotion Promotion { get; set; } = null!;

    public virtual User Supplier { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
