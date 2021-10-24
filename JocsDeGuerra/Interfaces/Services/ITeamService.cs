using JocsDeGuerra.Models;
using System.Collections.Generic;

namespace JocsDeGuerra.Interfaces.Services
{
    public interface ITeamService
    {
        List<TeamAsset> GetTeamAssets();
    }
}