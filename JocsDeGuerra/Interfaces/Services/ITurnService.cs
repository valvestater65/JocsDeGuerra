using JocsDeGuerra.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JocsDeGuerra.Interfaces.Services
{
    public interface ITurnService
    {
        Task<bool> AddTurn(Turn turn);
        Task<Turn> GetById(Guid id);
        Task<List<Turn>> GetTurns();
        Task<bool> UpdateTurn(Turn turn);
        Task<bool> TurnsExist();
    }
}