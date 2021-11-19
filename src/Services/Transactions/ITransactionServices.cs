using System;
using System.IO;
using System.Threading.Tasks;
using Data.Entities;
using Data.Commands;
using Data.Entities.Contracts;
using Data.Entities.Enums;

namespace Services.Transactions
{
    public interface ITransactionServices
    {
        Task<PageSortedList<TransactionModel>> GetTransactions(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc);
        Task Import(Stream stream);
        Task<Transaction> Categorize(string id, string catcode);
        Task<Transaction> Add(Transaction transaction);
        Task<Transaction> Update(Transaction transaction);
        Task Split(string id, SplitTransactionCommand command);
    }
}