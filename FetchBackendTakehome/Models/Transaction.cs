namespace FetchBackendTakehome.Models
{
    //Model for the Transaction, contains information on the payer, the points for the payer and the timestamp
    public class Transaction
    {
        public string Payer { get; set; }
        public int Points { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
