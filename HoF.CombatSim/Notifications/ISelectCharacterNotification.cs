using System.Collections.Generic;
using System.Linq;
using HoF.CombatSim.Model;
using Prism.Interactivity.InteractionRequest;

namespace HoF.CombatSim.Notifications
{
    public interface ISelectCharacterNotification : INotification
    {
        IOrderedEnumerable<Character> Characters { get; set; }
        Character SelectedCharacter { get; set; }
    }
}