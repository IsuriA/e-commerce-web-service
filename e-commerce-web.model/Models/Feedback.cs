using System;
using System.Collections.Generic;

namespace e_commerce_web.core.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int ProductId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
