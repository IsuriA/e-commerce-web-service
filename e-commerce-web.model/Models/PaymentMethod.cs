using System;
using System.Collections.Generic;

namespace e_commerce_web.core.Models;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
