using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.MerchantTypes
{
    public class MerchantTypeRepository : IMerchantTypeRepository
    {
        private readonly AppDbContext _context;

        public MerchantTypeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(MerchantType mcc)
        {
            var item = await _context.MerchantTypes.FindAsync(mcc.Code);

            if(item != null)
            {
                _context.Entry<MerchantType>(item).State = EntityState.Detached;
                await Update(mcc);
            }
            else
            {
                await _context.MerchantTypes.AddRangeAsync(mcc);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(MerchantType mcc)
        {
            _context.MerchantTypes.Update(mcc);
            await _context.SaveChangesAsync();
        }
    }
}