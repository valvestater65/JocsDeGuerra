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
        private readonly ISessionStorageService _sessionService;
        private readonly IAssetService _assetService;
        private readonly IMapLocationService _mapLocationService;
        private readonly string _dbKey = "/informeTorn";
        private readonly string _sessionKey = "informeTorns";

        public InformeTornViewModelService(IApiService apiService, ISessionStorageService sessionStorage,
            IAssetService assetService, IMapLocationService mapLocationService)
        {
            _apiService = apiService;
            _sessionService = sessionStorage;
            _assetService = assetService;
            _mapLocationService = mapLocationService;
        }

        public async Task<bool> AddInformeTorn(List<InformeTornViewModel> viewModel)
        {
            try
            {
                var informesSession = await _sessionService.GetItemAsync<List<InformeTornViewModel>>(_sessionKey);

                if (informesSession != null)
                {
                    await _sessionService.RemoveItemAsync(_sessionKey);
                }

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
                var informeTorns = await GetAll();

                if (informeTorns == null || informeTorns.Count == 0)
                    return null;

                return informeTorns.FirstOrDefault(x => x.Team.Id == team.Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<InformeTornViewModel> GetbyTeamAndTurn(Team team, Turn currentTurn)
        {
            try
            {
                var informeTorn = (await GetAll())
                    ?.Where(it => it.Team.Id == team.Id && it.CurrentTurn.Id == currentTurn.Id).FirstOrDefault();

                if (informeTorn == null)
                {
                    var retInforme = await CreateInformeTorn(team, currentTurn);
                    return retInforme;
                }

                return informeTorn;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<InformeTornViewModel> CreateInformeTorn(Team team, Turn turn)
        {
            var teamTurn = turn.Teams.Find(x => x.Id == team.Id);

            var newInforme = new InformeTornViewModel
            {
                CurrentTurn = turn,
                Team = teamTurn,
                TurnActions = new TurnActionsViewModel {TeamId = teamTurn.Id},
                Assets = await _assetService.GetAssets(),
                MapLocations = await _mapLocationService.GetLocations(),
                Id = Guid.NewGuid()
            };

            var informeTorns = await GetAll();

            if (informeTorns == null)
            {
                informeTorns = new List<InformeTornViewModel>();
            }

            informeTorns.Add(newInforme);

            await AddInformeTorn(informeTorns);
            return newInforme;
        }

        public async Task<List<InformeTornViewModel>> GetAll()
        {
            try
            {
                var sessionInfo = await _sessionService.GetItemAsync<List<InformeTornViewModel>>(_sessionKey);

                if (sessionInfo != null)
                    return sessionInfo;

                var informeTornStr = await _apiService.Get(_dbKey);

                if (!string.IsNullOrEmpty(informeTornStr) && informeTornStr != "null")
                {
                    var dbInformeTorn = JsonSerializer.Deserialize<List<InformeTornViewModel>>(informeTornStr);
                    await _sessionService.SetItemAsync(_sessionKey, dbInformeTorn);
                    return dbInformeTorn;
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
                if (await _apiService.Delete(_dbKey))
                {
                    if (await _sessionService.ContainKeyAsync(_sessionKey))
                    {
                        await _sessionService.RemoveItemAsync(_sessionKey);
                    }

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SaveInformeTorn(InformeTornViewModel viewModel)
        {
            try
            {
                var informeTorns = await GetAll();

                if (informeTorns != null)
                {
                    var oldinformeTorn = informeTorns.FirstOrDefault(x => x.Id == viewModel.Id);

                    if (oldinformeTorn != null)
                    {
                        informeTorns.Remove(oldinformeTorn);
                    }

                    informeTorns.Add(viewModel);
                    await AddInformeTorn(informeTorns);

                    return true;
                }


                await AddInformeTorn(new List<InformeTornViewModel> {viewModel});

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
