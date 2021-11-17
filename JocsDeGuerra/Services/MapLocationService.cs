using Blazored.SessionStorage;
using JocsDeGuerra.Constants;
using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace JocsDeGuerra.Services
{
    public class MapLocationService : IMapLocationService
    {
        private readonly IApiService _apiService;
        private readonly ISessionStorageService _sessionService;
        private const string LOCATION_URI = "/locations";
        private const string SESSION_KEY = "locations";

        public MapLocationService(IApiService api,ISessionStorageService session)
        {
            _apiService = api;
            _sessionService = session;
        }


        public async Task<bool> LocationsExist()
        {
            try
            {
                var locations = await GetLocations();
                return locations != null && locations.Count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ClearLocations()
        {
            try
            {
                return await _apiService.Delete(LOCATION_URI);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<MapLocation>> GetLocations()
        {
            try
            {
                var locationList = await _sessionService.GetItemAsync<List<MapLocation>>(SESSION_KEY);

                if (locationList != null && locationList.Count > 0)
                    return locationList;
                
                var locationstr = await _apiService.Get(LOCATION_URI);

                if (string.IsNullOrEmpty(locationstr))
                    return null;
                
                var locations = JsonSerializer.Deserialize<List<MapLocation>>(locationstr);
                await _sessionService.SetItemAsync(SESSION_KEY, locations);

                return locations;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> InitializeLocations()
        {
            try
            {
                if (await LocationsExist())
                    await ClearLocations();


                var locations = new List<MapLocation>
                {
                    new MapLocation{
                        Name = "Crusader",
                        ProductionPoints = 8,
                        ResearchPoints = 2,
                        ExplorationPoints = 2,
                        BattleArchetipes = BattleArchetype.COMBAT_NAUS,
                        Id = Guid.NewGuid()
                    },
                    new MapLocation{
                        Name = "Cellin",
                        ProductionPoints = 6,
                        ResearchPoints = 2,
                        ExplorationPoints = 2,
                        BattleArchetipes = BattleArchetype.FPS,
                        Id = Guid.NewGuid()
                    },
                    new MapLocation{
                        Name = "Daymar",
                        ProductionPoints = 6,
                        ResearchPoints = 2,
                        ExplorationPoints = 2,
                        BattleArchetipes = BattleArchetype.FPS,
                        Id = Guid.NewGuid()
                    },
                    new MapLocation{
                        Name = "Yela",
                        ProductionPoints = 6,
                        ResearchPoints = 2,
                        ExplorationPoints = 2,
                        BattleArchetipes = BattleArchetype.FPS_AGREST,
                        Id = Guid.NewGuid()
                    },
                    new MapLocation{
                        Name = "Port Olisar",
                        ProductionPoints = 2,
                        ResearchPoints = 6,
                        ExplorationPoints = 4,
                        BattleArchetipes = BattleArchetype.FPS_0G,
                        Id = Guid.NewGuid()
                    },
                    new MapLocation{
                        Name = "ARC-L3",
                        ProductionPoints = 2,
                        ResearchPoints = 6,
                        ExplorationPoints = 4,
                        BattleArchetipes = BattleArchetype.COMBAT_NAUS,
                        Id = Guid.NewGuid()
                    },
                    new MapLocation{
                        Name = "HUR-L3",
                        ProductionPoints = 2,
                        ResearchPoints = 6,
                        ExplorationPoints = 4,
                        BattleArchetipes = BattleArchetype.COMBAT_NAUS,
                        Id = Guid.NewGuid()
                    },
                    new MapLocation{
                        Name = "CRU-L1",
                        ProductionPoints = 0,
                        ResearchPoints = 0,
                        ExplorationPoints = 0,
                        Id = Guid.NewGuid()
                    }
                };

                var result = await _apiService.Put(LOCATION_URI, locations);
                return result == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
