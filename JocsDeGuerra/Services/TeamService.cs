using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
using System.Collections.Generic;

namespace JocsDeGuerra.Services
{
    public class TeamService : ITeamService
    {
        private readonly IAssetService _assetService;
        private readonly IMapLocationService _mapLocationService;

        public TeamService(IAssetService assetService, IMapLocationService mapLocationService)
        {
            _assetService = assetService;
            _mapLocationService = mapLocationService;
        }

        public List<TeamAsset> GetTeamAssets()
        {
            var assets = _assetService.GetAllAssets();
            var retList = new List<TeamAsset>();

            assets.ForEach(x =>
            {
                retList.Add(new TeamAsset
                {
                    Asset = x,
                    Available = 0,
                    Reserved = 0
                });
            });

            return retList;
        }
    }
}
