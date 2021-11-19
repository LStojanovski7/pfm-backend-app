using System.Collections.Generic;
using Data.Entities.Enums;

namespace Data.Entities
{
    public class Transaction
    {
        public string Id { get; set; }
         public string BeneficiaryName { get; set; }
         public string Date { get; set; }
         public Direction Direction { get; set; }
         public double Amount { get; set; }
         public string Description { get; set; }
         public string Currency { get; set; }
         public int? Mcc { get; set; }
         public TransactionKind Kind { get; set; }
         public string CategoryCode { get; set; }
         public virtual Category Category { get; set; }
         public virtual ICollection<TransactionSplit> Splits { get; set; }
    }
}