namespace Data.Repositories.Transactions
{
    public class TransactionsRepository : ITransactionsRepository
    {
        public readonly AppDbContext _context;

        public TransactionsRepository(AppDbContext context)
        {
            _context = context;
        }

        
    }
}