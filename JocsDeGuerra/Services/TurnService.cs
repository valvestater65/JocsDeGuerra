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
        private const string TURN_URI = "/turn";

        public TurnService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Turn>> GetTurns()
        {
            try
            {
                var dbTurns = await _apiService.Get(TURN_URI);

                if (!string.IsNullOrEmpty(dbTurns))
                {
                    return JsonSerializer.Deserialize<List<Turn>>(dbTurns);
                }

                return null;

            }
            catch (Exception)
            {
                return new List<Turn>();
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

        public async Task<bool> AddTurn(Turn turn)
        {
            try
            {
                var result = await _apiService.Post(TURN_URI, turn);
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
                var result = await _apiService.Put(TURN_URI, turn);

                return result < 0;
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


    }
}
