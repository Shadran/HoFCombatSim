using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoF.CombatSim.Notifications
{
    public class SelectCharacterNotification : Confirmation, ISelectCharacterNotification
    {
        public IOrderedEnumerable<Model.Character> Characters { get; set; }
        
        public Model.Character SelectedCharacter { get; set; }
    }
}
