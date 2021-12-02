using JocsDeGuerra.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace JocsDeGuerra.Services
{
    public class AdminSystemService : IAdminSystemService
    {
        private readonly ITurnService _turnService;
        private readonly ITeamService _teamService;
        private readonly IMapLocationService _mapLocationService;
        private readonly IAssetService _assetService;


        public AdminSystemService(ITurnService turnService, ITeamService teamService, IMapLocationService locationService, IAssetService assetService)
        {
            _turnService = turnService;
            _teamService = teamService;
            _mapLocationService = locationService;
            _assetService = assetService;
        }

        public async Task<bool> IsDataInitialized()
        {
            try
            {
                var locations = await _mapLocationService.LocationsExist();
                var turns = await _turnService.TurnsExist();
                var teams = await _teamService.TeamsExist();

                return locations && turns && teams;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> InitializeAllData()
        {
            try
            {
                await _teamService.InitializeTeams();
                await _mapLocationService.InitializeLocations();
                await _assetService.InitializeAssets();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SetMapLocations()
        {
            try
            {
                if (!await _mapLocationService.LocationsExist())
                {
                    await _mapLocationService.InitializeLocations();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
