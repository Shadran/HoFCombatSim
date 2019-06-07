using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoF.CombatSim.Model
{
    public class Character : BindableBase
    {
        private string _name;
        private Room _room;

        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public Room Room { get => _room; set => SetProperty(ref _room, value); }
        public ObservableCollection<Character> Allies { get; private set; } = new ObservableCollection<Character>();
        public ObservableCollection<Character> Enemies { get; private set; } = new ObservableCollection<Character>();
    }
}
