using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
using JocsDeGuerra.Models.ViewModels;

namespace JocsDeGuerra.Services
{
    public class InformeTornViewModelService : IInformeTornViewModelService
    {
        private readonly IApiService _apiService;
        private readonly ITurnService _turnService;
        private readonly ISessionStorageService _sessionService;
        private readonly string _dbKey = "/informeTorn";
        private readonly string _sessionKey = "informeTorns";

        public InformeTornViewModelService(IApiService apiService, ITurnService turnService, ISessionStorageService sessionStorage)
        {
            _apiService = apiService;
            _turnService = turnService;
            _sessionService = sessionStorage;
        }

        public async Task<bool> AddInformeTorn(InformeTornViewModel viewModel)
        {
            try
            {
                var result = await _apiService.Put(_dbKey, viewModel);

                return result == 0;


            }
            catch (Exception)
            {

                return false;
            }
        }


        public async Task<InformeTornViewModel> GetbyTeam(Team team)
        {
            try
            {
                var informeTorns = await GetInformeTorn();

                if (informeTorns == null || informeTorns.Count == 0)
                    return null;

                return informeTorns.Where(x => x.Team.Id == team.Id).FirstOrDefault();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<InformeTornViewModel> GetbyTeamAndTurn(Team team, Turn currentTurn)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InformeTornViewModel>> GetInformeTorn()
        {
            try
            {
                var sessionInfo = await _sessionService.GetItemAsync<List<InformeTornViewModel>>(_sessionKey);

                if (sessionInfo != null)
                    return sessionInfo;

                var informeTornStr = await _apiService.Get(_dbKey);

                if (!string.IsNullOrEmpty(informeTornStr))
                {
                    return JsonSerializer.Deserialize<List<InformeTornViewModel>>(informeTornStr);
                }

                return null;
            }


            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> Remove()
        {
            try
            {
                return await _apiService.Delete(_dbKey);

            }
            catch (Exception)
            {
                return false;
            }
        }

        Task<InformeTornViewModel> IInformeTornViewModelService.GetInformeTorn()
        {
            throw new NotImplementedException();
        }
    }
}
