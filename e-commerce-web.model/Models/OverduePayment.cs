using System;
using System.Collections.Generic;

namespace e_commerce_web.model.Models;

public partial class OverduePayment
{
    public string OverduePaymentId { get; set; } = null!;

    public string Amount { get; set; } = null!;

    public DateTime OverdueDate { get; set; }

    public string DebtorId { get; set; } = null!;

    public string InterestRate { get; set; } = null!;

    public string TotaAmount { get; set; } = null!;

    public DateTime FinalDate { get; set; }
}
