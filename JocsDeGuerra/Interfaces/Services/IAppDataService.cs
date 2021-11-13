using JocsDeGuerra.Models;
using System;

namespace JocsDeGuerra.Interfaces.Services
{
    public interface IAppDataService
    {
        Turn CurrentTurn { get; set; }
        Team SelectedTeam { get; set; }
        event Action TeamChanged;
    }
}