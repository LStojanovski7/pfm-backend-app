using Data.Entities;
using CsvHelper.Configuration;
using System.Globalization;

namespace Services.CsvMaps
{
    public class MccMap : ClassMap<MccCsv>
    {
        public MccMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Code).Name("code");
            Map(m => m.Type).Name("merchant-type");
        }
    }
}