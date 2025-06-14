using System;
using System.Collections.Generic;

namespace e_commerce_web.model.Models;

public partial class InquiryStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
}
