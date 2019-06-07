using CommonServiceLocator;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoF.CombatSim.Modules
{
    public class RoomHandlingModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainRegion", typeof(Views.RoomsList));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Room>();
            containerRegistry.RegisterForNavigation<Views.Character>();
            containerRegistry.RegisterForNavigation<Views.EncountersList>();
            containerRegistry.RegisterSingleton<Model.ICharactersCache, Model.CharactersCache>();
        }
    }
}
