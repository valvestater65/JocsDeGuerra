using JocsDeGuerra.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JocsDeGuerra.Interfaces.Services
{
    public interface IMapLocationService
    {
        Task<bool> LocationsExist();
        Task<List<MapLocation>> GetLocations();
        Task<bool> InitializeLocations();
        Task<bool> ClearLocations();
    }
}
