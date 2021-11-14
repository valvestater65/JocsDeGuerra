using JocsDeGuerra.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JocsDeGuerra.Interfaces.Services
{
    public interface IAssetService
    {
        
        Task<bool> InitializeAssets();
        Task<List<Asset>> GetAssets();
    }
}