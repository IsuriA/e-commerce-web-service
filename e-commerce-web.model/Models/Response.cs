using System;
using System.Collections.Generic;

namespace e_commerce_web.core.Models;

public partial class Response
{
    public int Id { get; set; }

    public string Reply { get; set; } = null!;

    public DateTime Date { get; set; }

    public int InquiryId { get; set; }

    public int StaffId { get; set; }
}
