namespace e_commerce_web.core.Models;

public partial class Payment
{
    public int Id { get; set; }

    public string Reference { get; set; }

    public int MethodId { get; set; }

    public virtual PaymentMethod Method { get; set; } = null!;

    public decimal Amount { get; set; }

    public int OrderId { get; set; }

    public int Installment { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public string ShippingReceiver { get; set; } = null!;

    public string ShippingEmail { get; set; } = null!;

    public string ShippingPhone { get; set; }

    public string ShippingReceiverNic { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public string SpecialInstructions { get; set; }
}
