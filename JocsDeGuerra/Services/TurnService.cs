using Blazored.SessionStorage;
using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
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
        public TurnService(IApiService apiService,ITeamService teamService, AppDataService appdata, ISessionStorageService session)
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
            catch (Exception ex)
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
                var result = await _apiService.Put(TURN_URI, turns);
                return result < 0;
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
                if (_appData.CurrentTurn != null)
                { 
                    return _appData.CurrentTurn.Teams.Where(x => x.Id == teamId).FirstOrDefault();
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
