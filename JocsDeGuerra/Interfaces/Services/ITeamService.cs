using JocsDeGuerra.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JocsDeGuerra.Interfaces.Services
{
    public interface ITeamService
    {
        List<TeamAsset> GetTeamAssets();
        Task InitializeTeams();
    }
}