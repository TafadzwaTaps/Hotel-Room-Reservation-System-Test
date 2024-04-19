namespace Hotel_Room_Reservation_System_Test.Models
{
    public class CancellationPolicy
    {
        public int id { get; set; }
        public string? PolicyName { get; set; }
        public string? PolicyDescription { get; set; }
        public int DaysBeforeCheckIn { get; set; }

        public decimal CancellationFee { get; set; }

        public CancellationPolicy()
        {
            DaysBeforeCheckIn = 0;
        }

        public CancellationPolicy(string _policyName, string _policyDescription, int _daysBeforeCheckIn, decimal cancellationFee)
        {
            PolicyName = _policyName;
            PolicyDescription = _policyDescription;
            DaysBeforeCheckIn = _daysBeforeCheckIn;
            CancellationFee = cancellationFee;
        }
    }
}
