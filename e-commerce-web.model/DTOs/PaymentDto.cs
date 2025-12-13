using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e_commerce_web.core.Models;

namespace e_commerce_web.core.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }

        public string Reference { get; set; }

        public virtual PaymentMethodDto Method { get; set; }

        public decimal Amount { get; set; }

        public int OrderId { get; set; }

        public int Installment { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? CreatedUser { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string ShippingReceiver { get; set; } = null!;

        public string ShippingEmail { get; set; } = null!;

        public string ShippingPhone { get; set; }

        public string ShippingReceiverNic { get; set; }

        public string ShippingAddress { get; set; } = null!;

        public string SpecialInstructions { get; set; }
    }
}
