using JocsDeGuerra.Constants;
using JocsDeGuerra.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JocsDeGuerra.Services
{
    public class AdminSystemService : IResetSystemService
    {
        private readonly ITurnService _turnService;
        private readonly ITeamService _teamService;


        public AdminSystemService(ITurnService turnService, ITeamService teamService)
        {
            _turnService = turnService;
            _teamService = teamService;
        }



        public async Task<bool> IsDataInitialized()
        {
            try
            {
                return await _turnService.TurnsExist();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task SetTeams()
        { 

        }

    }
}
