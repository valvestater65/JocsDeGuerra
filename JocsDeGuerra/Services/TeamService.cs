using JocsDeGuerra.Constants;
using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JocsDeGuerra.Services
{
    public class TeamService : ITeamService
    {
        private readonly IAssetService _assetService;
        private readonly IMapLocationService _mapLocationService;
        private readonly HttpClient _httpClient;

        public TeamService(IAssetService assetService, IMapLocationService mapLocationService, IHttpClientFactory clientFactory)
        {
            _assetService = assetService;
            _mapLocationService = mapLocationService;
            _httpClient = clientFactory.CreateClient(NamedClients.FIREBASE_CLIENT);
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


        public async Task InitializeTeams()
        {
            try
            {

                var teamList = new List<Team> {
                    new Team{
                        Id = Guid.NewGuid(),
                        Name ="Equip Vermell",
                        BaseExplorationPoints = 3,
                        BaseProductionPoints = 4,
                        BaseResearchPoints = 5
                    },
                    new Team{
                        Id = Guid.NewGuid(),
                        Name ="Equip Blanc",
                        BaseExplorationPoints = 3,
                        BaseProductionPoints = 4,
                        BaseResearchPoints = 5
                    }
                };

                var testTeam = new Team
                {
                    Name = "test"
                };

                var content = new StringContent(JsonSerializer.Serialize(teamList));
                await _httpClient.PostAsync($"/teams.json?auth=T8hyHa2kgzsIIJdLVvNW8kF6pGOgVqOE5ViJOIrP", content);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
