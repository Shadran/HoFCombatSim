using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace HoF.CombatSim.Model
{
    public interface ICharactersCache : ICollection<Character>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        new void Add(Character character);
        Character GetByName(string characterName);
        new void Remove(Character character);
    }
}