using JocsDeGuerra.Constants;
using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
using System;
using System.Collections.Generic;

namespace JocsDeGuerra.Services
{
    public class MapLocationService : IMapLocationService
    {
        public IEnumerable<MapLocation> GetAllLocations()
        {
            return new List<MapLocation>
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
        }
    }
}
