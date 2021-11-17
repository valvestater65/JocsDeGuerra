using JocsDeGuerra.Models;
using JocsDeGuerra.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JocsDeGuerra.Interfaces.Services
{
    public interface IInformeTornViewModelService
    {
        Task<bool> AddInformeTorn(List<InformeTornViewModel> viewModel);
        Task<bool> Remove();
        Task<InformeTornViewModel> GetbyTeam(Team team);
        Task<InformeTornViewModel> GetbyTeamAndTurn(Team team, Turn currentTurn);
        Task<bool> SaveInformeTorn(InformeTornViewModel viewModel);
        Task<List<InformeTornViewModel>> GetAll();
    }
}