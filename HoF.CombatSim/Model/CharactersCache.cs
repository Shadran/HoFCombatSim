using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoF.CombatSim.Model
{
    public class CharactersCache : ObservableCollection<Character>, ICharactersCache
    {
        public CharactersCache(IEnumerable<Character> characters) : base(characters)
        {
        }

        public Character GetByName(string characterName)
        {
            return this.FirstOrDefault(x => x.Name == characterName);
        }

        public new void Add(Character character)
        {
            if (this.Any(x => x == character)) return;
            base.Add(character);
        }

        public new void Remove(Character character)
        {
            foreach(var x in this)
            {
                x.Allies.Remove(character);
                x.Enemies.Remove(character);
            }
            base.Remove(character);
        }
    }
}
