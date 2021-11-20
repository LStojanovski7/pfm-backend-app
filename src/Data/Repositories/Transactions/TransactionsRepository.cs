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
        public async Task<PageSortedList<TransactionWithSplits>> Get(int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.Asc)
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

            List<TransactionWithSplits> splitsItems = new List<TransactionWithSplits>();

            foreach(var item in items)
            {
                TransactionWithSplits split = new TransactionWithSplits();

                split.Id = item.Id;
                split.BeneficiaryName = item.BeneficiaryName;
                split.Date = item.Date;
                split.Direction = item.Direction;
                split.Amount = item.Amount;
                split.Description = item.Description;
                split.Currency = item.Currency;
                split.Mcc = item.Mcc;
                split.CatCode = item.CategoryCode;

                List<SingleCategorySplit> splitsList = new List<SingleCategorySplit>();

                foreach(var s in item.Splits)
                {
                    SingleCategorySplit singleCategorySplit = new SingleCategorySplit();

                    singleCategorySplit.CatCode = s.CategoryCode;
                    singleCategorySplit.Amount = s.Amount;

                    splitsList.Add(singleCategorySplit);
                }

                split.Splits = splitsList;

                splitsItems.Add(split);
            }

            return new PageSortedList<TransactionWithSplits>()
            {
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy,
                SortOrder = sortOrder,
                TotalCount = total,
                TotalPages = totalPages == 0 ? 1 : totalPages,
                Items = splitsItems,
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