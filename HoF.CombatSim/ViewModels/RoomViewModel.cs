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
        private enum Tabs
        {
            CharacterTab = 0,
            EncounersTab = 1
        }

        private readonly IRegionManager _regionManager;
        private readonly ICharactersCache _characters;
        private Room _room;
        private Character _selectedCharacter;
        private int _selectedTab;

        public Room Room { get => _room; set => SetProperty(ref _room, value); }
        public int SelectedTab { get => _selectedTab; set => SetProperty(ref _selectedTab, value); }
        public Character SelectedCharacter
        {
            get => _selectedCharacter;
            set
            {
                SetProperty(ref _selectedCharacter, value);
                if (_selectedCharacter == null)
                {
                    DeactivateRegion("CharacterDetailsRegion");
                }
                else
                {
                    _regionManager.RequestNavigate("CharacterDetailsRegion", "Character", new NavigationParameters { { "character", _selectedCharacter } });
                }
            }
        }

        public DelegateCommand NavigateCommand { get; private set; }

        public void NavigateBack()
        {
            _regionManager.RequestNavigate("MainRegion", "RoomsList");
        }


        public DelegateCommand AddCharacterCommand { get; private set; }

        void AddCharacter()
        {
            if (Room == null) return;
            var newCharacter = new Character { Room = Room };
            _characters.Add(newCharacter);
            Room.Players.Add(newCharacter);
            SelectedCharacter = newCharacter;
            SelectedTab = (int)Tabs.CharacterTab;
        }

        public DelegateCommand<Character> RemoveCharacterCommand { get; private set; }

        void RemoveCharacter(Character character)
        {
            if (character == null) return;
            _characters.Remove(character);
            Room.Players.Remove(character);
        }

        bool CanRemoveCharacter(Character character)
        {
            return character != null;
        }

        public RoomViewModel(IRegionManager regionManager, ICharactersCache characters)
        {
            _regionManager = regionManager;
            _characters = characters;
            NavigateCommand = new DelegateCommand(NavigateBack);
            AddCharacterCommand = new DelegateCommand(AddCharacter);
            RemoveCharacterCommand = new DelegateCommand<Character>(RemoveCharacter, CanRemoveCharacter).ObservesProperty(() => SelectedCharacter);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["room"] is Room room)
                Room = room;
            _regionManager.RequestNavigate("RoomEncountersRegion", "EncountersList", new NavigationParameters { { "room", Room } });
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["room"] is Room room)
                return Room != null && Room == room;
            else
                return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            SelectedCharacter = null;
            DeactivateRegion("RoomEncountersRegion");
        }

        private void DeactivateRegion(string regionName)
        {
            var activeView = _regionManager.Regions[regionName].ActiveViews.FirstOrDefault();
            if (activeView != null)
                _regionManager.Regions[regionName].Deactivate(activeView);
        }
    }
}
