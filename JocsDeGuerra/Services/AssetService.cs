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
                    BaseProductionCost = 3,
                    BaseResearchCost =0,
                    Enabled = true
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Caces mitjans",
                    Abbrv = "CM",
                    BaseProductionCost = 4,
                    BaseResearchCost =0,
                    Enabled = true
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Caces pesats",
                    Abbrv = "CP",
                    BaseProductionCost = 6,
                    BaseResearchCost =16,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Gladiator/Harbringer",
                    Abbrv = "BL",
                    BaseProductionCost = 7,
                    BaseResearchCost =16,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Redeemer",
                    Abbrv = "RD",
                    BaseProductionCost = 8,
                    BaseResearchCost =16,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Eclipse",
                    Abbrv = "EC",
                    BaseProductionCost = 12,
                    BaseResearchCost =16,
                    Enabled = false
                },
                new Asset{
                    Id = Guid.NewGuid(),
                    Name = "Retaliator",
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
                    Abbrv = "EC",
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
