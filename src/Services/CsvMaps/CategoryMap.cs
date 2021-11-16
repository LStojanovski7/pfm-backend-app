using CsvHelper.Configuration;
using System.Globalization;
using Data.Entities.CSVObjects;

namespace Services.CsvMaps
{
    public class CategoryMap : ClassMap<CategoriesCSV>
    {
        public CategoryMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Code).Name("code");
            Map(m => m.ParentCode).Name("parent-code");
            Map(m => m.Name).Name("name");
        }
    }
}