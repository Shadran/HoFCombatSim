using HoF.CombatSim.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoF.CombatSim.ViewModels
{
    public class RoomViewModel : BindableBase, INavigationAware
    {
        private Room _selectedRoom;

        public Room SelectedRoom { get => _selectedRoom; set => SetProperty(ref _selectedRoom, value); }
        public Character SelectedCharacter { get => _selectedCharacter; set => SetProperty(ref _selectedCharacter, value); }

        private DelegateCommand _navigateCommand;
        private readonly IRegionManager _regionManager;

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigateCommand));

        public void ExecuteNavigateCommand()
        {
            _regionManager.RequestNavigate("MainRegion", "RoomsList");
        }

        private DelegateCommand<Character> _characterSelectedCommand;
        public DelegateCommand<Character> CharacterSelectedCommand =>
            _characterSelectedCommand ?? (_characterSelectedCommand = new DelegateCommand<Character>(ExecuteCharacterSelectedCommand));

        void ExecuteCharacterSelectedCommand(Character character)
        {
            var parameters = new NavigationParameters();
            parameters.Add("character", character);
            _regionManager.RequestNavigate("CharacterDetailsRegion", "Character", parameters);
        }

        private DelegateCommand _addCharacterCommand;
        private Character _selectedCharacter;

        public DelegateCommand AddCharacterCommand =>
            _addCharacterCommand ?? (_addCharacterCommand = new DelegateCommand(ExecuteAddCharacterCommand));

        void ExecuteAddCharacterCommand()
        {
            if (SelectedRoom == null) return;
            var newCharacter = new Character();
            SelectedRoom.Players.Add(newCharacter);
            SelectedCharacter = newCharacter;
        }

        public RoomViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var room = navigationContext.Parameters["room"] as Room;
            if (room != null)
                SelectedRoom = room;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var room = navigationContext.Parameters["room"] as Room;
            if (room != null)
                return SelectedRoom != null && SelectedRoom == room;
            else
                return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
