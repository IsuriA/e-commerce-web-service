﻿using System;
using System.Collections.Generic;

namespace e_commerce_web.model.Models;

public partial class OrderStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
