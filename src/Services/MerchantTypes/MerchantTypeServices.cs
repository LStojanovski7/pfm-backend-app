using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Data.Entities;
using Data.Entities.CSVObjects;
using Data.Repositories;
using Services.CsvMaps;

namespace Services.MerchantTypes
{
    public class MerchantTypeService : IMerchantTypeService
    {
        private readonly IRepository<MerchantType> _repository;
        public MerchantTypeService(IRepository<MerchantType> repository)
        {
            _repository = repository;
        }
        public async Task Import(Stream stream, FileInfo file = null)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            List<MccCsv> result = new List<MccCsv>();

            using(StreamReader reader = new StreamReader(stream))
            using(var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csvReader.Context.RegisterClassMap<MccMap>();
                result = csvReader.GetRecords<MccCsv>().ToList<MccCsv>();
            }

            foreach(var item in result)
            {
                MerchantType mcc = new MerchantType();

                mcc.Code = item.Code;
                mcc.Type = item.Type;

                await _repository.Add(mcc);
            } 
        }
    }
}