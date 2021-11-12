using Data.Repositories.Transactions;
using Data.Entities;

namespace Services.Transactions
{
    public class TransactionServices : ITransactionServices
    {
        private readonly ITransactionsRepository _repository;

        public TransactionServices(ITransactionsRepository repository)
        {
            _repository = repository;
        }
    }
}