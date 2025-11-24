using Ecommerce.DTOs;
using Ecommerce.Interface;

namespace Ecommerce.Services
{
    public class PaymentService : IPaymentService
    {
        public PaymentResponseDTO ProcessPayment(PaymentRequestDTO request)
        {
            Console.WriteLine($"Method: {request.PaymentMethod}");
            Console.WriteLine($"Value: {request.Amount}");
            switch (request.PaymentMethod.ToLower())
            {
                case "cartão":
                    return ProcessCardPayment(request.Card, request.Amount);

                case "pix":
                    return ProcessPixPayment(request.Pix, request.Amount);

                case "boleto":
                    return ProcessBoletoPayment(request.Boleto, request.Amount);

                default:
                    return new PaymentResponseDTO
                    {
                        Status = "error",
                        Message = "Invalid payment method.",
                    };
            }
        }

        private PaymentResponseDTO ProcessBoletoPayment(BoletoDto boleto, decimal amount)
        {
            if (boleto == null)
                return Error("\r\nPayment slip details not provided.");

            return new PaymentResponseDTO
            {
                Status = "pending",
                Message = "Payment slip successfully generated.",
                TransactionId = Guid.NewGuid().ToString(),
                BoletoNumber = "23793.38127 60000.000009 04000.123456 7 78940000010000",
                BoletoUrl = "https://example.com/boleto/123456",
                Amount = amount,
                PaymentMethod = "boleto",
            };
        }

        private PaymentResponseDTO ProcessPixPayment(PixDTO pix, decimal amount)
        {
            if (pix == null)
                return Error("\r\nPIX data not provided.");
            return new PaymentResponseDTO
            {
                Status = "pending",
                Message = "PIX generated successfully.",
                TransactionId = Guid.NewGuid().ToString(),
                PixCode = $"00020126360014BR.GOV.BCB.PIX0114+5511999999995204000053039865404100",
                Amount = amount,
                PaymentMethod = "pix",
            };
        }

        private PaymentResponseDTO ProcessCardPayment(CardDTO card, decimal amount)
        {
            if (card == null)
                return Error("Card details not provided.");

            return new PaymentResponseDTO
            {
                Status = "approved",
                Message = "Card payment approved.",
                TransactionId = Guid.NewGuid().ToString(),
                Amount = amount,
                PaymentMethod = "card",
            };
        }

        private PaymentResponseDTO Error(string msg) =>
            new PaymentResponseDTO { Status = "error", Message = msg };
    }
}
