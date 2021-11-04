using System.Threading.Tasks;

namespace JocsDeGuerra.Interfaces.Services
{
    public interface IResetSystemService
    {
        Task<bool> IsDataInitialized();
    }
}