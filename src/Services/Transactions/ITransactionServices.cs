using System.IO;
using System.Threading.Tasks;
using Data.Entities;

namespace Services.Transactions
{
    public interface ITransactionServices
    {
        Task Import(Stream stream);
        Task<Transaction> Add(Transaction transaction);
        Task<Transaction> Update(Transaction transaction);
    }
}