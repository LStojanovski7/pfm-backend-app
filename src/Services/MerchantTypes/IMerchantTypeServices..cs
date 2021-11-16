using System.IO;
using System.Threading.Tasks;

namespace Services.MerchantTypes
{
    public interface IMerchantTypeService
    {
        Task Import(Stream stream, FileInfo file = null);
    }
}