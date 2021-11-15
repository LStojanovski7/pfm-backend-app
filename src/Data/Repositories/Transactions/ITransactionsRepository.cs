using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories.Transactions
{
    public interface ITransactionsRepository
    {
        Task<Transaction> GetTransaction(string id);
        Task<Transaction> Add(Transaction transaction);
        Task<Transaction> Update(Transaction transaction);
    }
}