using System.Threading.Tasks;
using Data.Entities.Enums;
using Data.Entities.Contracts;
using Data.Entities;
using System;
using System.Collections.Generic;

namespace Data.Repositories.Transactions
{
    public interface ITransactionsRepository
    {
        Task<PageSortedList<TransactionWithSplits>> Get(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc);
        Task<Transaction> GetTransaction(string id);
        Task<Transaction> Add(Transaction transaction);
        Task<Transaction> Update(Transaction transaction);
        Task Split(TransactionSplit split);
        Task<List<Transaction>> GetAll();
    }
}