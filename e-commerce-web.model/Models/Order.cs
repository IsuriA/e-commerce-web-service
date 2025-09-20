using System;
using System.Collections.Generic;

namespace e_commerce_web.core.Models;

public partial class Order
{
    public int Id { get; set; }

    public int StatusId { get; set; }

    public decimal Amount { get; set; }

    public int UserId { get; set; }

    public int? DeliveryPersonId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User DeliveryPerson { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual OrderStatus Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
