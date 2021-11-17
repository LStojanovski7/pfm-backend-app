using System;
using System.IO;
using System.Threading.Tasks;
using Data.Entities;
using Data.Entities.Contracts;
using Data.Entities.Enums;

namespace Services.Transactions
{
    public interface ITransactionServices
    {
        Task<PageSortedList<Transaction>> GetTransactions(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc);
        Task Import(Stream stream);
        Task<Transaction> Add(Transaction transaction);
        Task<Transaction> Update(Transaction transaction);
    }
}