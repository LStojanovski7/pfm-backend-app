using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using System.IO;
using Data.Repositories.Transactions;
using Data.Entities;
using Data.Entities.Enums;
using Services.CsvMaps;
using CsvHelper;
using CsvHelper.Configuration;

namespace Services.Transactions
{
    public class TransactionServices : ITransactionServices
    {
        private readonly ITransactionsRepository _repository;

        public TransactionServices(ITransactionsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Transaction> Add(Transaction transaction)
        {
            await _repository.Add(transaction);

            return transaction;
        }

        public async Task<Transaction> Update(Transaction transaction)
        {
            var entity = await _repository.GetTransaction(transaction.Id);

            if(entity == null)
            {
                throw new KeyNotFoundException("No such transaction");
            }
            else
            {
                await _repository.Update(transaction);
            }
            
            return transaction;
        }

        public async Task Import(Stream stream)
        {
            List<TransactionCSV> result = new List<TransactionCSV>();

             CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);

             csvConfiguration.UseNewObjectForNullReferenceMembers = true;

            using(var reader = new StreamReader(stream))
            using(var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csvReader.Context.RegisterClassMap<TransactionMap>();
                result = csvReader.GetRecords<TransactionCSV>().ToList<TransactionCSV>();
            }

            //Automapper needed
            //TODO: validations

            foreach(var item in result)
            {
                Transaction transaction = new Transaction();

                transaction.Id = item.Id;
                transaction.BeneficiaryName = item.BeneficiaryName;
                transaction.Date = item.Date;
                transaction.Direction = Enum.Parse<Direction>(item.Direction);
                //TEMPORARY SOLUTION
                transaction.Amount = ConvertToNumber(item.Amount);
                transaction.Description = item.Description;
                transaction.Currency = item.Currency.Trim();

                int number;

                bool parsed = int.TryParse(item.Mcc, out number);

                if(parsed == false)
                {
                    transaction.Mcc = null;
                }
                else
                {
                    transaction.Mcc = number;
                }

                transaction.Kind = Enum.Parse<TransactionKind>(item.Kind);

                await _repository.Add(transaction);
            }
        }

        private double ConvertToNumber(string amount)
        {
            amount = amount.Trim();

            var output = double.Parse(amount.Replace("€", ""));

            return output;
        }
    }
}