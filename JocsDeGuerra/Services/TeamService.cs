using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace JocsDeGuerra.Services
{
    public class TeamService : ITeamService
    {
        private readonly IAssetService _assetService;
        private readonly IMapLocationService _mapLocationService;
        private readonly string _projectId;
        private readonly FirestoreDb _context;

        public TeamService(IAssetService assetService, IMapLocationService mapLocationService)
        {
            _assetService = assetService;
            _mapLocationService = mapLocationService;
            _projectId = "jocsdeguerra";
            _context = FirestoreDb.Create(_projectId);


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
                var testTeam = new Team
                {
                    Name = "test"
                };

                await _context.Collection("teams").AddAsync(JsonSerializer.Serialize(testTeam));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
