namespace Sample_Hotel_Room_Reservation_System.Models
{
    public class InvoiceGeneration
    {
        public int Id { get; set; }
        public string ReservationID { get; set; }

        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public InvoiceGeneration()
        {
            TotalAmount = 0;
        }

        public InvoiceGeneration(string _reservationID, decimal _totalAmount,string _Status)
        {
            ReservationID = _reservationID;
            TotalAmount = _totalAmount;
            Status = _Status;
        }
    }
}
