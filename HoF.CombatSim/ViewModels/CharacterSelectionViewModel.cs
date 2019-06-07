using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoF.CombatSim.ViewModels
{
    public class CharacterSelectionViewModel : BindableBase, IInteractionRequestAware
    {
        private Model.Character _selectedCharacter;
        private Notifications.ISelectCharacterNotification _notification;

        public DelegateCommand<Model.Character> SelectCharacterCommand { get; private set; }
        public DelegateCommand CancelSelectionCommand { get; private set; }

        void SelectCharacter(Model.Character character)
        {
            _notification.SelectedCharacter = character;
            FinishInteraction?.Invoke();
        }

        bool CanSelectCharacter(Model.Character character)
        {
            return character != null;
        }

        private void CancelSelection()
        {
            FinishInteraction?.Invoke();
        }

        public Model.Character SelectedCharacter { get => _selectedCharacter; set => SetProperty(ref _selectedCharacter, value); }
        public INotification Notification { get => _notification; set => SetProperty(ref _notification, (Notifications.ISelectCharacterNotification)value); }
        public Action FinishInteraction { get; set; }

        public CharacterSelectionViewModel()
        {
            SelectCharacterCommand = new DelegateCommand<Model.Character>(SelectCharacter, CanSelectCharacter).ObservesProperty(() => SelectedCharacter);
            CancelSelectionCommand = new DelegateCommand(CancelSelection);
        }
    }
}
