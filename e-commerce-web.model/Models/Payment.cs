using System;
using System.Collections.Generic;

namespace e_commerce_web.core.Models;

public partial class Payment
{
    public string PaymentId { get; set; } = null!;

    public string Method { get; set; } = null!;

    public int Amount { get; set; }

    public string OrderId { get; set; } = null!;
}
