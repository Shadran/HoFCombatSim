using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoF.CombatSim.Model
{
    public class EncountersMatcher
    {
        private readonly ICharactersCache _characters;

        public event EventHandler EncountersChanged;

        public List<Encounter> GetRoomEncounters(Room room)
        {
            if (room == null) return new List<Encounter>();
            return room.Players
                .SelectMany(attacker => 
                attacker.Enemies.Where(defender => defender.Room == room)
                .Select(defender => new Encounter { Attacker = attacker, Defender = defender })).ToList();
        }

        public EncountersMatcher(ICharactersCache characters)
        {
            _characters = characters;
            _characters.CollectionChanged += _characters_CollectionChanged;
        }

        private void _characters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems.OfType<Character>())
                {
                    item.Allies.CollectionChanged -= Related_CollectionChanged;
                    item.Enemies.CollectionChanged -= Related_CollectionChanged;
                }
            }
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.OfType<Character>())
                {
                    item.Allies.CollectionChanged += Related_CollectionChanged;
                    item.Enemies.CollectionChanged += Related_CollectionChanged;
                }
            }
            EncountersChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Related_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            EncountersChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
