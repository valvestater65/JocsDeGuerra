using Blazored.SessionStorage;
using JocsDeGuerra.Constants;
using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
using System;

namespace JocsDeGuerra.Services
{
    public class AppDataService : IAppDataService
    {

        private readonly ISyncSessionStorageService _sessionService;

        public AppDataService(ISyncSessionStorageService sessionService)
        {
            _sessionService = sessionService;
        }


        public Team SelectedTeam
        {
            get => GetSelectedTeamFromSession();
            set
            {
                _sessionService.SetItem(SharedSessionKeys.SelectedTeam, value);
                NotifyStateChanged();
            }
        }

        public Turn CurrentTurn { get; set; }

        public event Action TeamChanged;
        private void NotifyStateChanged() => TeamChanged?.Invoke();



        private Team GetSelectedTeamFromSession()
        {
            var team =  _sessionService.GetItem<Team>(SharedSessionKeys.SelectedTeam);
            
            return team ?? null;    
        }
    }
}
