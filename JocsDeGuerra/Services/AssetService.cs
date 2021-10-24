using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JocsDeGuerra.Services
{
    public class AssetService : IAssetService
    {

        public List<Asset> GetAllAssets()
        {
           
            return new List<Asset> {
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Caces bàsics",
                    Abbrv = "CB",
                    BaseProductionCost = 2,
                    BaseResearchCost =0,
                    Enabled = true
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Caces lleugers",
                    Abbrv = "CL",
                    BaseProductionCost = 2,
                    BaseResearchCost =0,
                    Enabled = true
                }
            };
            
        }
    }
}
