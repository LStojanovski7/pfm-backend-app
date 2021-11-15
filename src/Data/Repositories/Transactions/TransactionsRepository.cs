using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Transactions
{
    public class TransactionsRepository : ITransactionsRepository
    {
        public readonly AppDbContext _context;

        public TransactionsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction> GetTransaction(string id) => await _context.Transactions.FindAsync(id);

        public async Task<Transaction> Add(Transaction transaction)
        {
            var entity = await _context.Transactions.FindAsync(transaction.Id);

            if(entity != null)
            {
                _context.Entry<Transaction>(entity).State = EntityState.Detached;
                
                await Update(transaction);
                
                return transaction;
            }

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction> Update(Transaction transaction)
        {
            _context.Update(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }
    }
}