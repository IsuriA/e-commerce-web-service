namespace e_commerce_web.core.DTOs
{
    public class CheckoutDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string NIC { get; set; }
        public string Address { get; set; }
        public string Instructions { get; set; }
        public string PaymentMethod { get; set; }
        public string Reference { get; set; }
        public int Installments { get; set; }
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public int? CustomerId { get; set; }
    }
}
