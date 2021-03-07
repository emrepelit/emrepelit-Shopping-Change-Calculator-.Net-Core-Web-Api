using CalculateShoppingChange.Business;
using CalculateShoppingChange.Business.Models;
using System.Threading.Tasks;

namespace CalculateShoppingChange.Abstract
{
    public interface IChangeService
    {
       ApiResponse<GetChangeResponse> GetChange(Transaction transaction);
        
    }
}
