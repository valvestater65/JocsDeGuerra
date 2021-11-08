using JocsDeGuerra.Constants;
using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JocsDeGuerra.Services
{
    public class AdminSystemService : IAdminSystemService
    {
        private readonly ITurnService _turnService;
        private readonly ITeamService _teamService;
        private readonly IMapLocationService _mapLocationService; 


        public AdminSystemService(ITurnService turnService, ITeamService teamService, IMapLocationService locationService)
        {
            _turnService = turnService;
            _teamService = teamService;
            _mapLocationService = locationService;
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

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //Initial team load. 
        public async Task SetTeams()
        {
            try
            {
                



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
