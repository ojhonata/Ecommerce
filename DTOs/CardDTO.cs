namespace Ecommerce.DTOs
{
    public class CardDTO
    {
        public string CardNumber { get; set; }
        public string HolderName { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
        public int Installments { get; set; }
    }
}
