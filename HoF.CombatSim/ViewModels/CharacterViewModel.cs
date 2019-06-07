using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoF.CombatSim.ViewModels
{
    public class CharacterViewModel : BindableBase, INavigationAware
    {
        private Model.Character _character;
        private Model.Character _selectedAlly;
        private Model.Character _selectedEnemy;
        private readonly Model.ICharactersCache _characters;

        public Model.Character Character { get => _character; set => SetProperty(ref _character, value); }
        public Model.Character SelectedAlly { get => _character; set => SetProperty(ref _selectedAlly, value); }
        public Model.Character SelectedEnemy { get => _character; set => SetProperty(ref _selectedEnemy, value); }

        public InteractionRequest<INotification> NotificationRequest { get; private set; } = new InteractionRequest<INotification>();

        public InteractionRequest<Notifications.ISelectCharacterNotification> SelectCharacterRequest { get; private set; } = new InteractionRequest<Notifications.ISelectCharacterNotification>();

        public DelegateCommand AddAllyCommand { get; private set; }

        private void AddAlly()
        {
            var possibleAllies = _characters.Where(x => x != Character && !_character.Allies.Contains(x) && !_character.Enemies.Contains(x)).OrderBy(x => x.Name);
            if (!possibleAllies.Any())
            {
                NotificationRequest.Raise(new Notification
                {
                    Content = "No character available for this operation!",
                    Title = "No Character Available"
                });
                return;
            }
            SelectCharacterRequest.Raise(new Notifications.SelectCharacterNotification
            {
                Title = "Character Selection",
                Content = "",
                Characters = possibleAllies
            },
            n =>
            {
                if (n != null && n.SelectedCharacter != null)
                {
                    Character.Allies.Add(n.SelectedCharacter);
                }  
            });
        }

        public DelegateCommand<Model.Character> RemoveAllyCommand { get; private set; }

        private void RemoveAlly(Model.Character ally)
        {
            if (ally == null) return;
            Character.Allies.Remove(ally);
        }

        private bool CanRemoveAlly(Model.Character ally)
        {
            return ally != null;
        }

        public DelegateCommand AddEnemyCommand { get; private set; }

        private void AddEnemy()
        {
            var possibleEnemies = _characters.Where(x => x != Character && !_character.Allies.Contains(x) && !_character.Enemies.Contains(x)).OrderBy(x => x.Name);
            if (!possibleEnemies.Any())
            {
                NotificationRequest.Raise(new Notification
                {
                    Content = "No character available for this operation!",
                    Title = "No Character Available"
                });
                return;
            }
            SelectCharacterRequest.Raise(new Notifications.SelectCharacterNotification
            {
                Title = "Character Selection",
                Content = "",
                Characters = possibleEnemies
            },
            n =>
            {
                if (n != null && n.SelectedCharacter != null)
                {
                    Character.Enemies.Add(n.SelectedCharacter);
                }
            });
        }

        public DelegateCommand<Model.Character> RemoveEnemyCommand { get; private set; }

        private void RemoveEnemy(Model.Character enemy)
        {
            if (enemy == null) return;
            Character.Enemies.Remove(enemy);
        }

        private bool CanRemoveEnemy(Model.Character enemy)
        {
            return enemy != null;
        }

        public CharacterViewModel(Model.ICharactersCache characters)
        {
            _characters = characters;
            AddAllyCommand = new DelegateCommand(AddAlly);
            RemoveAllyCommand = new DelegateCommand<Model.Character>(RemoveAlly, CanRemoveAlly).ObservesProperty(() => SelectedAlly);
            AddEnemyCommand = new DelegateCommand(AddEnemy);
            RemoveEnemyCommand = new DelegateCommand<Model.Character>(RemoveEnemy, CanRemoveEnemy).ObservesProperty(() => SelectedEnemy);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var character = navigationContext.Parameters["character"] as Model.Character;
            if (character != null)
                return Character != null && Character == character;
            else
                return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var character = navigationContext.Parameters["character"] as Model.Character;
            if (character != null)
                Character = character;
        }
    }
}
