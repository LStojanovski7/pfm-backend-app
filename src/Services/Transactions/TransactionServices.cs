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
using Data.Entities.Contracts;
using Services.Categories;

namespace Services.Transactions
{
    public class TransactionServices : ITransactionServices
    {
        private readonly ITransactionsRepository _repository;
        private readonly ICategoryServices _categoryService;

        public TransactionServices(ITransactionsRepository repository, ICategoryServices categoryService)
        {
            _repository = repository;
            _categoryService = categoryService;
        }
        public async Task<PageSortedList<TransactionWithSplits>> GetTransactions(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc)
        {
            var result = await _repository.Get(page, pageSize, sortBy, sortOrder); 

            return result;
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

            //TODO: validations

            foreach(var item in result)
            {
                Transaction transaction = new Transaction();

                transaction.Id = item.Id;
                transaction.BeneficiaryName = item.BeneficiaryName;
                transaction.Date = item.Date;
                transaction.Direction = Enum.Parse<Direction>(item.Direction);
                transaction.Amount = Double.Parse(item.Amount);
                transaction.Description = item.Description;
                transaction.Currency = item.Currency;

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
        public async Task<Transaction> Categorize(string id, string catcode)
        {
            var transaction = await _repository.GetTransaction(id);
            var category = await _categoryService.GetCategory(catcode);

            if(transaction == null || category == null)
            {
                return null;
            }

            transaction.CategoryCode = category.Code;
            transaction.Category = category;

            await _repository.Update(transaction);

            return transaction;
        }
        public async Task<Transaction> Split(string id, List<SingleCategorySplit> splits)
        {
            var transaction = await _repository.GetTransaction(id);

            double totalAmount = transaction.Amount;

            foreach(var item in splits)
            {
                var category = await _categoryService.GetCategory(item.CatCode);

                if(category == null) continue;

                TransactionSplit split = new TransactionSplit();

                if(totalAmount > 0 && item.Amount <= totalAmount)
                {
                    split.Amount = item.Amount;
                }
                else
                {
                    return null;
                }

                split.CategoryCode = item.CatCode;
                split.TransactionId = transaction.Id;

                totalAmount -= item.Amount;

                await _repository.Split(split);
            }

            if(totalAmount > 0)
            {
                TransactionSplit split = new TransactionSplit();

                split.CategoryCode = null;

                split.Amount = totalAmount;
                split.TransactionId = transaction.Id;

                await _repository.Split(split);
            }

            transaction.CategoryCode = null;
            await _repository.Update(transaction);
            
            return transaction;
        }
    }
}