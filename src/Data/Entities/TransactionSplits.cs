namespace Data.Entities
{
    public class TransactionSplit
    {
        public string Id { get; set; }
        public double Amount { get; set; }

        public string CategoryCode { get; set; }
        public virtual Category Category { get; set; }

        public string TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}