namespace Sample_Hotel_Room_Reservation_System.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public int TransactionId { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public string CurrencyCode { get; set; }

        public Payment()
        {
            Amount = 0;
        }

        public Payment(int _reservationID, decimal _amount, string _paymentMethod, int _transactionID, string _paymentStatus)
        {
            ReservationId = _reservationID;
            Amount = _amount;
            PaymentMethod = _paymentMethod;
            TransactionId = _transactionID;
            PaymentStatus = _paymentStatus;          
        }
    }
}
