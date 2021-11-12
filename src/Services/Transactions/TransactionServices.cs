using Services.Interfaces;
using Data.Repositories.Transactions;
using Data.Entities;

namespace Services
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