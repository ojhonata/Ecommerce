namespace Ecommerce.DTOs
{
    public class PaymentRequestDTO
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethod { get; set; }

        public CardDTO Card { get; set; }
        public PixDTO Pix { get; set; }
        public BoletoDto Boleto { get; set; }
    }
}
