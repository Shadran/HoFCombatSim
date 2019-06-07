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
    public class RoomsListViewModel : BindableBase
    {

        private DelegateCommand<Room> _navigateCommand;
        private Room _selectedRoom;
        private readonly IRegionManager _regionManager;
        private readonly ICharactersCache _characters;

        public DelegateCommand<Room> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<Room>(ExecuteNavigateCommand));

        public ObservableCollection<Room> Rooms { get; private set; } = new ObservableCollection<Room>();
        public Room SelectedRoom { get => _selectedRoom; set => SetProperty(ref _selectedRoom, value); }

        private void ExecuteNavigateCommand(Room room)
        {
            if (room == null) return;
            var parameters = new NavigationParameters();
            parameters.Add("room", room);
            _regionManager.RequestNavigate("MainRegion", "Room", parameters);
        }

        private DelegateCommand<Room> _removeRoomCommand;
        public DelegateCommand<Room> RemoveRoomCommand =>
            _removeRoomCommand ?? (_removeRoomCommand = new DelegateCommand<Room>(ExecuteRemoveRoomCommand, CanExecuteRemoveRoomCommand).ObservesProperty(() => SelectedRoom));

        void ExecuteRemoveRoomCommand(Room room)
        {
            if (room == null) return;
            foreach(var character in room.Players)
            {
                _characters.Remove(character);
            }
            Rooms.Remove(room);
        }

        bool CanExecuteRemoveRoomCommand(Room room)
        {
            return room != null;
        }

        public RoomsListViewModel(IRegionManager regionManager, ICharactersCache characters)
        {
            _regionManager = regionManager;
            _characters = characters;
            CreateRooms();
        }

        private void CreateRooms()
        {
            Rooms.Add(new Room
            {
                Name = "Test Room 1"
            });
            Rooms.Add(new Room
            {
                Name = "Test Room 2"
            });
            Rooms.Add(new Room
            {
                Name = "Test Room 3"
            });
            Rooms.Add(new Room
            {
                Name = "Test Room 4"
            });
            Rooms.Add(new Room
            {
                Name = "Test Room 5"
            });
        }
    }
}
