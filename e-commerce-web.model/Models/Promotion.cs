using System;
using System.Collections.Generic;

namespace e_commerce_web.model.Models;

public partial class Promotion
{
    public int Id { get; set; }

    public int Discount { get; set; }

    public int ProductId { get; set; }

    public string? UserId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
