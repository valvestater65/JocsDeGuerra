using JocsDeGuerra.Interfaces.Services;
using JocsDeGuerra.Models;
using System;

namespace JocsDeGuerra.Services
{
    public class AppDataService : IAppDataService
    {
        private Team _selectedTeam;
        public Team SelectedTeam
        {
            get => _selectedTeam; 
            set
            {
                _selectedTeam = value;
                NotifyStateChanged();
            }
        }
        public Turn CurrentTurn { get; set; }

        public event Action TeamChanged;
        private void NotifyStateChanged() => TeamChanged?.Invoke();
    }
}
