using System;
using System.Collections.Generic;

namespace e_commerce_web.core.Models;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
