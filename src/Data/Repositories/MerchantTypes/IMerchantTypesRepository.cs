using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories.MerchantTypes
{
    public interface IMerchantTypeRepository
    {
        Task Add(MerchantType mcc);
        Task Update(MerchantType mcc);
    }
}