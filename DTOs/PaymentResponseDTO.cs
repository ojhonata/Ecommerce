namespace Ecommerce.DTOs
{
    public class PaymentResponseDTO
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public string TransactionId { get; set; }

        // PIX
        public string PixCode { get; set; }

        // Boleto
        public string BoletoNumber { get; set; }
        public string BoletoUrl { get; set; }

        // Valor da compra
        public decimal Amount { get; set; }

        // Tipo de pagamento usado (Card, Pix, Boleto)
        public string PaymentMethod { get; set; }
    }
}
