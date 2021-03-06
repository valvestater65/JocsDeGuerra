using Blazored.SessionStorage;
using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
using JocsDeGuerra.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace JocsDeGuerra.Services
{
    public class TurnService : ITurnService
    {
        private readonly IApiService _apiService;
        private readonly ITeamService _teamService;
        private const string TURN_URI = "/turns";
        private readonly ISessionStorageService _sessionStorage;
        private readonly IInformeTornViewModelService _informeTornViewModelService;
        private string _sessionKey;
        public TurnService(IApiService apiService,ITeamService teamService, ISessionStorageService session, IInformeTornViewModelService informeTornViewModelService)
        {
            _apiService = apiService;
            _teamService = teamService;
            _sessionStorage = session;
            _sessionKey = "turns";
            _informeTornViewModelService = informeTornViewModelService;
        }

        public async Task<List<Turn>> GetTurns()
        {
            try
            {

                var dbTurns = await _sessionStorage.GetItemAsync<List<Turn>>(_sessionKey);

                if (dbTurns == null)
                {
                    var dbTurnsstr = await _apiService.Get(TURN_URI);

                    if (!string.IsNullOrEmpty(dbTurnsstr))
                    {
                        dbTurns = JsonSerializer.Deserialize<List<Turn>>(dbTurnsstr);

                        if (dbTurns != null)
                        {
                            await _sessionStorage.SetItemAsync(_sessionKey, dbTurns);
                        }
                    }
                }

                return dbTurns?? new List<Turn>();

            }
            catch (JsonException)
            {
                return new List<Turn>();
            }
            catch (Exception)
            {
                return new List<Turn>();
            }
        }

        public async Task<Turn> GetCurrentTurn()
        {
            try
            {
                var turns = await GetTurns();
                Turn currentTurn;

                if (turns == null || turns.Count == 0)
                {
                    currentTurn = await CreateTurn(true);
                }
                else 
                {
                    currentTurn = turns.FirstOrDefault(x => x.Current);
                }

                await _sessionStorage.SetItemAsync($"{_sessionKey}_{currentTurn.Id}",currentTurn);

                return currentTurn;
            }
            catch (Exception)
            {

                return new Turn();
            }
        }

        /*Not sure I can query by Id directly*/
        public async Task<Turn> GetById(Guid id)
        {
            try
            {
                var turns = await GetTurns();

                if (turns == null)
                {
                    return null;
                }

                var turn = turns.FirstOrDefault(x => x.Id == id);
                return turn ?? null;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AddTurns(List<Turn> turns)
        {
            try
            {
                //TODO: May need to save in session here? 
                var result = await _apiService.Put(TURN_URI, turns);

                if (result == 0)
                { 
                    await _sessionStorage.RemoveItemAsync(_sessionKey);
                }

                return result == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Turn> GetPreviousTurn(Turn turn)
        {
            try
            {
                var turns = await GetTurns();
                
                return turns.FirstOrDefault(x => x.TurnNumber == turn.TurnNumber - 1);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> UpdateTurn(Turn turn)
        {
            try
            {
                var result = await _apiService.Patch(TURN_URI, turn);

                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> TurnsExist()
        {
            try
            {
                var turns = await GetTurns();
                return turns != null && turns.Count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Turn> CreateTurn(bool current)
        {
            try
            {
                var turns = await GetTurns();
                
                if (turns == null)
                    turns = new List<Turn>();

                var turnTeams = await _teamService.GetTeams();
                
                var turn = new Turn
                {
                    Teams = turnTeams,
                    Battle = new Battle(),
                    Completed = false,
                    Current = current,
                    Id = Guid.NewGuid(),
                    TurnNumber = turns.Count + 1
                };

                //Initialize Team points when turn 1
                if (turn.TurnNumber == 1)
                {
                    turn.Teams.ForEach(t =>
                    {
                        t.AvailablePoints.ProductionPoints = t.BaseProductionPoints;
                        t.AvailablePoints.ResearchPoints = t.BaseResearchPoints;
                        t.AvailablePoints.ExplorationPoints = t.BaseExplorationPoints;
                    });
                }

                if (current)
                {
                    turns.ForEach(t => t.Current = false);
                }

                turns.Add(turn);

                _ = await AddTurns(turns);

                return turn;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Team> GetCurrentTurnTeam(Guid teamId)
        {
            try
            {
                var turnList = await GetCurrentTurn();    

                
                if (turnList != null)
                { 
                    return turnList.Teams.FirstOrDefault(x => x.Id == teamId);
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> SaveTurn()
        {
            try
            {
                var informeTorns = await _informeTornViewModelService.GetAll();

                if (informeTorns == null)
                {
                    return false;
                }

                var readyness = informeTorns.Select(it => it.Team.ReadyToClose);
                

                if (readyness.Any(x => !x))
                {
                    //Els dos equips no estan ready, no podem tancar torn. 
                    return false;
                }

                
                var currentTurn = await GetCurrentTurn();
                var newTurn = await CreateTurn(true);

                newTurn.Teams.ForEach(t => {
                    var informeTeam = informeTorns.FirstOrDefault(it => it.Team.Id == t.Id);

                    //Available points calc.
                    t.AvailablePoints = CalculateAvailablePoints(currentTurn, informeTeam);

                    //Setting owned locations new to false
                    informeTeam.Team.OwnedLocations.ForEach(ol => ol.New = false);
                    t.OwnedLocations = informeTeam.Team.OwnedLocations;
                    t.AvailableAssets = informeTeam.Team.AvailableAssets;

                });
                currentTurn.TurnActions = informeTorns.Select(x => x.TurnActions).ToList();

                //Torn previ
                await UpdateTurnList(currentTurn);

                //Nou torn.
                return await UpdateTurnList(newTurn);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private AccumulatedPoints CalculateAvailablePoints(Turn currentTurn, InformeTornViewModel informeTeam)
        {
            try
            {
                var currentTeamPoints = currentTurn.Teams.Where(te => te.Id == informeTeam.Team.Id).Select(x => x.AvailablePoints).FirstOrDefault();

                var ownedLocations = informeTeam.Team.OwnedLocations.Where(x => !x.New).ToList();

                var accumulated = new AccumulatedPoints
                { 
                    ExplorationPoints = (currentTeamPoints.ExplorationPoints - informeTeam.TurnActions.ExplorationPoints) + ownedLocations.Sum(x => x.Location.ExplorationPoints),
                    ProductionPoints = (currentTeamPoints.ProductionPoints - informeTeam.TurnActions.ProductionPoints) + ownedLocations.Sum(x => x.Location.ProductionPoints) + informeTeam.TurnActions.IndustryPoints,
                    ResearchPoints = (currentTeamPoints.ResearchPoints - informeTeam.TurnActions.ResearchPoints) + ownedLocations.Sum(x => x.Location.ResearchPoints)
                };

                return accumulated;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<bool> UpdateTurnList(Turn newTurn)
        {
            try
            {
                var oldTurns = await GetTurns();

                if (oldTurns == null)
                {
                    return await AddTurns(new List<Turn> { newTurn });
                }

                oldTurns.Remove(oldTurns.Find(x => x.Id == newTurn.Id));
                oldTurns.ForEach(t => t.Current = false);
                oldTurns.Add(newTurn);


                return await AddTurns(oldTurns);


            }
            catch (Exception)
            {

                return false;
            }
        }


    }
}
