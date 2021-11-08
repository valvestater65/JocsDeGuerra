using System.Threading.Tasks;

namespace JocsDeGuerra.Interfaces.Services
{
    public interface IAdminSystemService
    {
        Task<bool> IsDataInitialized();
        Task<bool> InitializeAllData();
    }
}