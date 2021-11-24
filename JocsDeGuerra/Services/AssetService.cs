using Blazored.SessionStorage;
using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JocsDeGuerra.Services
{
    public class AssetService : IAssetService
    {

        private readonly string _assetURI = "/assets";
        private readonly string _sessionKey = "dbassets";
        private readonly IApiService _apiService;
        private readonly ISessionStorageService _sessionService;

        public AssetService(IApiService apiService, ISessionStorageService sessionStorage)
        {
            _apiService = apiService;   
            _sessionService = sessionStorage;
        }



        public async Task<bool> InitializeAssets()
        {
            try
            {
                var result = await _apiService.Put(_assetURI, GetAllAssets());

                return result == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Asset>> GetAssets()
        {
            try
            {
                var assets = await _sessionService.GetItemAsync<List<Asset>>(_sessionKey);

                if (assets != null)
                    return assets;

                var dbAssetsStr = await _apiService.Get(_assetURI);

                if (!string.IsNullOrEmpty(dbAssetsStr))
                { 
                    var dbAssets = JsonSerializer.Deserialize<List<Asset>>(dbAssetsStr);
                    await _sessionService.SetItemAsync(_sessionKey, dbAssets);
                    return dbAssets;
                }

                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
            

        private List<Asset> GetAllAssets()
        {

            return new List<Asset> {
                new Asset{
                    Id = Guid.NewGuid(), /*aurora, mustang, reliant kore, 100, razor, m50, pisces, mantis*/
                    Name = "Caces bàsics",
                    Abbrv = "CB",
                    BaseProductionCost = 2,
                    BaseResearchCost =0,
                    Enabled = true
                },
                new Asset{
                    Id = Guid.NewGuid(), /*Arrow, gladius, sabre, talon, 300, avenger, reliant tana, vanduul blade, nomad,kartu-al, hawk, merlin,buccaneer*/
                    Name = "Caces lleugers",
                    Abbrv = "CL",
                    BaseProductionCost = 3,
                    BaseResearchCost =0,
                    Enabled = true
                },
                new Asset{
                    Id = Guid.NewGuid(), /*Cutty, Super Hornet, Vanduul Scite/glaive, defender*/
                    Name = "Caces mitjans",
                    Abbrv = "CM",
                    BaseProductionCost = 4,
                    BaseResearchCost =0,
                    Enabled = true
                },
                new Asset{
                    Id = Guid.NewGuid(), /*Vanguard Ares Freelancer MIS, prowler, Hurricane, mercury*/
                    Name = "Caces pesats",
                    Abbrv = "CP",
                    BaseProductionCost = 6,
                    BaseResearchCost =16,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Bombarder Lleuger (Gladiator/Harbringer)",
                    Abbrv = "BL",
                    BaseProductionCost = 7,
                    BaseResearchCost = 16,
                    Enabled = false
                },
                new Asset {
                    Id = Guid.NewGuid(),
                    Name = "Suport Lleuger (Connie)",
                    Abbrv = "CO",
                    BaseProductionCost = 7,
                    BaseResearchCost = 16,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Redeemer",
                    Abbrv = "RE",
                    BaseProductionCost = 10,
                    BaseResearchCost = 16,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Bombarder Mitjà (Eclipse)",
                    Abbrv = "BM",
                    BaseProductionCost = 12,
                    BaseResearchCost =16,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Bombarder Pesat (Retaliator)",
                    Abbrv = "BP",
                    BaseProductionCost = 20,
                    BaseResearchCost =28,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "HammerHead",
                    Abbrv = "HH",
                    BaseProductionCost = 28,
                    BaseResearchCost =28,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Dragonfly",
                    Abbrv = "DR",
                    BaseProductionCost = 2,
                    BaseResearchCost =8,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Cyclone",
                    Abbrv = "CY",
                    BaseProductionCost = 3,
                    BaseResearchCost =8,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Ursa",
                    Abbrv = "UR",
                    BaseProductionCost = 4,
                    BaseResearchCost =8,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Nova Tank",
                    Abbrv = "NT",
                    BaseProductionCost = 10,
                    BaseResearchCost =16,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Ballista",
                    Abbrv = "BA",
                    BaseProductionCost = 12,
                    BaseResearchCost =16,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Valkyrie",
                    Abbrv = "VA",
                    BaseProductionCost = 8,
                    BaseResearchCost =0,
                    Enabled = true
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Hercules M2",
                    Abbrv = "HE",
                    BaseProductionCost = 12,
                    BaseResearchCost =14,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Naus Respawn",
                    Abbrv = "NR",
                    BaseProductionCost = 6,
                    BaseResearchCost =0,
                    Enabled = true
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Terrapin",
                    Abbrv = "TE",
                    BaseProductionCost = 2,
                    BaseResearchCost =0,
                    Enabled = true
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Carrack",
                    Abbrv = "CA",
                    BaseProductionCost = 30,
                    BaseResearchCost =22,
                    Enabled = false
                }

            };
            
        }
    }
}
