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
    public class EncountersListViewModel : BindableBase, INavigationAware
    {
        private readonly Model.EncountersMatcher _matcher;

        public Model.Room Room { get; set; }

        public List<Model.Encounter> Encounters => _matcher.GetRoomEncounters(Room);

        public EncountersListViewModel(Model.EncountersMatcher matcher)
        {
            _matcher = matcher;
            _matcher.EncountersChanged += _matcher_EncountersChanged;
        }

        private void _matcher_EncountersChanged(object sender, EventArgs e)
        {
            this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(Encounters)));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["room"] is Model.Room room)
                return room != null && Room == room;
            else
                return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["room"] is Model.Room room)
                Room = room;
        }
    }
}
