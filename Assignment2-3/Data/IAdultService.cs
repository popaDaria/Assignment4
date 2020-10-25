using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment2_3.Models;

namespace Assignment2_3.Data
{
    public interface IAdultService
    {
        Task<IList<Adult>> GetAdultsAsync();
        Task AddAdultAsync(Adult newAdult);
        Task UpdateAdultInfoAsync(Adult newAdult);
        Task RemoveAdultAsync(Adult adult);
    }
}