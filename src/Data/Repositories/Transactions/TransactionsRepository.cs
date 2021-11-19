using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Entities.Enums;
using Data.Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Collections.Generic;

namespace Data.Repositories.Transactions
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly AppDbContext _context;
        public TransactionsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PageSortedList<TransactionModel>> Get(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc)
        {
            var query = _context.Transactions.AsQueryable();

            var total = await query.CountAsync();

            var totalPages = total = (int)Math.Ceiling(total * 1.0 / pageSize);

            if(!string.IsNullOrEmpty(sortBy))
            {
                if(sortOrder == SortOrder.Desc)
                {
                    query = query.OrderByDescending(sortBy, p => p.Id);
                }
                else
                {
                    query = query.OrderBy(sortBy, p => p.Id);
                    sortOrder = SortOrder.Asc;
                }
            }
            else
            {
                query = query.OrderBy(p => p.Id);
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var items = await query.ToListAsync();

            List<TransactionModel> transactions = new List<TransactionModel>();

            foreach(var item in items)
            {
                TransactionModel transaction = new TransactionModel();

                transaction.Id = item.Id;
                transaction.BeneficiaryName = item.BeneficiaryName;
                transaction.Date = item.Date;
                transaction.Direction = item.Direction;
                transaction.Amount = item.Amount;
                transaction.Description = item.Description;
                transaction.Currency = item.Currency;
                transaction.Mcc = item.Mcc;
                transaction.Kind = item.Kind;

                transactions.Add(transaction);
            }

            return new PageSortedList<TransactionModel>()
            {
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy,
                SortOrder = sortOrder,
                TotalCount = total,
                TotalPages = totalPages == 0 ? 1 : totalPages,
                Items = transactions,
            };
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

        public async Task Split(TransactionSplit split)
        {
            await _context.Splits.AddAsync(split);
            await _context.SaveChangesAsync();
        }
    }
}