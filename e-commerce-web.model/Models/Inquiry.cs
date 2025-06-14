using System;
using System.Collections.Generic;

namespace e_commerce_web.model.Models;

public partial class Inquiry
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Message { get; set; } = null!;

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public virtual InquiryStatus Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
