using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoF.CombatSim.Model
{
    public class Room : BindableBase
    {
        private string _name;

        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public ObservableCollection<Character> Players { get; private set; } = new ObservableCollection<Character>();
    }
}
