using JocsDeGuerra.Models;
using JocsDeGuerra.Models.ViewModels;
using System.Threading.Tasks;

namespace JocsDeGuerra.Interfaces.Services
{
    public interface IInformeTornViewModelService
    {
        Task<bool> AddInformeTorn(InformeTornViewModel viewModel);
        Task<InformeTornViewModel> GetInformeTorn();
        Task<bool> Remove();
        Task<InformeTornViewModel> GetbyTeam(Team team);
        Task<InformeTornViewModel> GetbyTeamAndTurn(Team team, Turn currentTurn);
    }
}