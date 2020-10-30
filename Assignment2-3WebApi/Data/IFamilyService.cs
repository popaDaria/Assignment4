using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment2_3WebApi.Models;

namespace Assignment2_3WebApi.Data
{
    public interface IFamilyService
    {
        Task<IList<Family>> GetFamiliesAsync();
        Task AddFamilyAsync(Family newFamily);
        Task<IList<int>> AdultsInFamiliesAsync();
        Task RemoveAdultAsync(int adultId);
    }
}