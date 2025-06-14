using System;
using System.Collections.Generic;

namespace e_commerce_web.model.Models;

public partial class DeliveryPerson
{
    public string DeliveryPersonId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;
}
