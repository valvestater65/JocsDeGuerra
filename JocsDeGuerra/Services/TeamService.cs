using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace JocsDeGuerra.Services
{
    public class TeamService : ITeamService
    {
        private readonly IAssetService _assetService;
        private readonly IMapLocationService _mapLocationService;
        private readonly IApiService _apiService;
        private const string TEAM_URL = "/teams";

        public TeamService(IAssetService assetService, IMapLocationService mapLocationService, IApiService apiService)
        {
            _assetService = assetService;
            _mapLocationService = mapLocationService;
            _apiService = apiService;
        }

        public async Task<List<TeamAsset>> GetTeamAssets()
        {
            try
            {
                var assets = await _assetService.GetAssets();

                if (assets == null)
                    return null;
                var retList = new List<TeamAsset>();

                assets.ForEach(x =>
                {
                    retList.Add(new TeamAsset
                    {
                        Asset = x,
                        Available = (x.Abbrv.ToUpper() == "TE") ? 1:0,
                        Reserved = 0
                    }) ;
                });

                return retList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> TeamsExist()
        {
            try
            {
                var teams = await GetTeams();
                return teams != null && teams.Count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CleanTeams()
        {
            try
            {
                return await _apiService.Delete(TEAM_URL);

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> InitializeTeams()
        {
            try
            {

                if (await TeamsExist()) {
                    await CleanTeams();
                }


                var teams = new List<Team> {
                    new Team{
                        Name = "Equip Vermell",
                        Id = Guid.NewGuid(),
                        BaseExplorationPoints = 4 ,
                        BaseProductionPoints = 6,
                        BaseResearchPoints = 3,
                        OwnedLocations = new List<OwnedLocation> {
                            new OwnedLocation {
                                Location = new MapLocation{
                                        Name ="Base Vermell",
                                        Id = Guid.NewGuid(),
                                        ProductionPoints = 2,
                                        ResearchPoints = 3,
                                        IsConquerable = false
                                }
                            }
                        },
                        AvailableAssets = await GetTeamAssets()
                    },
                    new Team{
                        Name = "Equip Blanc",
                        Id = Guid.NewGuid(),
                        BaseExplorationPoints = 4,
                        BaseProductionPoints = 6,
                        BaseResearchPoints = 3,
                        OwnedLocations = new List<OwnedLocation> {
                            new OwnedLocation {
                                Location = new MapLocation{
                                        Name ="Base Blanc",
                                        Id = Guid.NewGuid(),
                                        ProductionPoints = 2,
                                        ResearchPoints = 3,
                                        IsConquerable = false
                                }
                            }
                        },
                        AvailableAssets= await GetTeamAssets()
                    }
                };

                var result = await _apiService.Put(TEAM_URL, teams);
                return result == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CreateTeam(Team team)
        {
            try
            {
                var response = await _apiService.Put(TEAM_URL, team);
                return response == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Team>> GetTeams()
        {
            try
            {
                var teams = await _apiService.Get(TEAM_URL);

                if (string.IsNullOrEmpty(teams))
                    return null;

                var data = JsonSerializer.Deserialize<List<Team>>(teams);

                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
