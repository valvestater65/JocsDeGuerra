using JocsDeGuerra.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JocsDeGuerra.Interfaces.Services
{
    public interface ITeamService
    {
        List<TeamAsset> GetTeamAssets();
        Task<bool> CreateTeam(Team team);
        Task<List<Team>> GetTeams();
        Task<bool> TeamsExist();
        Task<bool> InitializeTeams();
        Task<bool> CleanTeams();
    }
}