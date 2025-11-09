using Ecommerce.DTOs;
using Ecommerce.Interface;

namespace Ecommerce.Services
{
    public class PaymentService : IPaymentService
    {
        public PaymentResponseDTO ProcessPayment(PaymentRequestDTO request)
        {
            Console.WriteLine($"método: {request.PaymentMethod}");
            Console.WriteLine($"Valor: {request.Amount}");
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
                        Message = "Método de pagamento inválido."
                    };
            }
        }

        private PaymentResponseDTO ProcessBoletoPayment(BoletoDTO boleto, decimal amount)
        {
            if (boleto == null)
                return Error("Dados do boleto não fornecidos.");

            return new PaymentResponseDTO
            {
                Status = "pending",
                Message = "Boleto gerado com sucesso.",
                TransactionId = Guid.NewGuid().ToString(),
                BoletoNumber = "23793.38127 60000.000009 04000.123456 7 78940000010000",
                BoletoUrl = "https://example.com/boleto/123456",
                Amount = amount,
                PaymentMethod = "boleto"
            };
        }

        private PaymentResponseDTO ProcessPixPayment(PixDTO pix, decimal amount)
        {
            if (pix == null)
                return Error("Dados do PIX não fornecidos.");
            return new PaymentResponseDTO
            {
                Status = "pending",
                Message = "PIX gerado com sucesso.",
                TransactionId = Guid.NewGuid().ToString(),
                PixCode = $"00020126360014BR.GOV.BCB.PIX0114+5511999999995204000053039865404100",
                Amount = amount,
                PaymentMethod = "pix"

            };
        }

        private PaymentResponseDTO ProcessCardPayment(CardDTO card, decimal amount)
        {
            if (card == null)
                return Error("Dados do cartão não fornecidos.");

            return new PaymentResponseDTO
            {
                Status = "approved",
                Message = "Pagamento com cartão aprovado.",
                TransactionId = Guid.NewGuid().ToString(),
                Amount = amount,
                PaymentMethod = "card"
            };
        }

        private PaymentResponseDTO Error(string msg) => new PaymentResponseDTO
        {
            Status = "error",
            Message = msg
        };
    }
}
