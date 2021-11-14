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
        private string _sessionKey;
        public TurnService(IApiService apiService,ITeamService teamService, ISessionStorageService session)
        {
            _apiService = apiService;
            _teamService = teamService;
            _sessionStorage = session;
            _sessionKey = "turns";

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
            Turn currentTurn = new();
            try
            {
                var turns = await GetTurns();

                if (turns == null || turns.Count == 0)
                {
                    currentTurn = await CreateTurn(true);
                }
                else 
                {
                    currentTurn = turns.Where(x => x.Current).FirstOrDefault();
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

                var turn = turns.Where(x => x.Id == id).FirstOrDefault();
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
                return result == 0;
            }
            catch (Exception)
            {
                return false;
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
                    return turnList.Teams.Where(x => x.Id == teamId).FirstOrDefault();
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> SaveTurn(InformeTornViewModel viewModel)
        {
            try
            {
                if (viewModel.CurrentTurn.Teams.Any(x => !x.ReadyToClose))
                {
                    //Els dos equips no estan ready, no podem tancar torn. 
                    return false;
                }

                var currentTurn = await GetCurrentTurn();

                var index = currentTurn.Teams.FindIndex(x => x.Id == viewModel.Team.Id);
                currentTurn.Teams.Remove(currentTurn.Teams.ElementAt(index));
                currentTurn.Teams.Add(viewModel.Team);  
                

                 





                return false;

                //TODO:
                /*
                 * 2. viewModel teams --> turn teams
                 * 3. Recalcular available points (nomes amb !New locations)
                 * 4. Current torn tancat (boolean true)
                 * 5. Crear nou torn (nou current torn)
                 * 6. OwnedLocation nou torn --> New = false
                 * 7. Update llista de turns. 
                 */
            }
            catch (Exception)
            {
                return false;
            }
        }

        private AccumulatedPoints CalculateAvailablePoints(Turn currentTurn, InformeTornViewModel viewModel)
        {
            throw new NotImplementedException();
        }


    }
}
