using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment2_3.Models;

namespace Assignment2_3.Data
{
    public interface IFamilyService
    {
        Task<IList<Family>> GetFamiliesAsync();
        Task AddFamilyAsync(Family newFamily);
        Task<IList<int>> AdultsInFamiliesAsync();
        Task RemoveAdultAsync(Adult adult);
    }
}