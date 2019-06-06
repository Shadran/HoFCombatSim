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
        private readonly IRegionManager _regionManager;

        public DelegateCommand<Room> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<Room>(ExecuteNavigateCommand));

        public ObservableCollection<Room> Rooms { get; private set; } = new ObservableCollection<Room>();

        private void ExecuteNavigateCommand(Room room)
        {
            if (room == null) return;
            var parameters = new NavigationParameters();
            parameters.Add("room", room);
            _regionManager.RequestNavigate("MainRegion", "Room", parameters);
        }

        public RoomsListViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
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
