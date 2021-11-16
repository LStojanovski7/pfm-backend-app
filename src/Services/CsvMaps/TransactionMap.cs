using Data.Entities;
using CsvHelper.Configuration;
using System.Globalization;
using Data.Entities.CSVObjects;

namespace Services.CsvMaps
{
    public class TransactionMap : ClassMap<TransactionCSV>
    {
        public TransactionMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Id).Name("id");
            Map(m => m.BeneficiaryName).Name("beneficiary-name");
            Map(m => m.Date).Name("date");
            Map(m => m.Direction).Name("direction");
            Map(m => m.Amount).Name("amount");
            Map(m => m.Description).Name("description");
            Map(m => m.Currency).Name("currency");
            Map(m => m.Mcc).Name("mcc");
            Map(m => m.Kind).Name("kind");
        }
    }
}