using Data.Entities.Enums;
using System.Collections.Generic;

namespace Data.Entities
{
    public class TransactionWithSplits
    {
        public string Id { get; set; }
        public string BeneficiaryName { get; set; }
        public string Date { get; set; }
        public string Direction { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public int? Mcc { get; set; }
        public string CatCode { get; set; }
        public List<SingleCategorySplit> Splits { get; set; }
    }
}