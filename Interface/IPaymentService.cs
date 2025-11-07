using Ecommerce.DTOs;

namespace Ecommerce.Interface
{
    public interface IPaymentService
    {
        public PaymentResponseDTO ProcessPayment(PaymentRequestDTO request);
    }
}
