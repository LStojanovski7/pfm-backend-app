using System.Threading.Tasks;
using Services.MerchantTypes;

namespace Services
{
    public class DbInitialization
    {
        private readonly IMerchantTypeService _merchantTypeService;
        public DbInitialization(IMerchantTypeService merchantTypeService)
        {
            _merchantTypeService = merchantTypeService;
        }

        // public async Task SeedData()
        // {
        //    _merchantTypeService.Import(); 
        // }
    }
}