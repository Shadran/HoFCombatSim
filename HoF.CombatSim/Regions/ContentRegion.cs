using Prism.Regions;
using Prism.Regions.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HoF.CombatSim.Regions
{
    public class ContentRegion : ContentControlRegionAdapter
    {

        public ContentRegion(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, ContentControl regionTarget)
        {
            base.Adapt(region, regionTarget);
        }

        protected override void AttachBehaviors(IRegion region, ContentControl regionTarget)
        {
            base.AttachBehaviors(region, regionTarget);
        }

        protected override void AttachDefaultBehaviors(IRegion region, ContentControl regionTarget)
        {
            base.AttachDefaultBehaviors(region, regionTarget);
            RegionBehaviorFactory.AddIfMissing("DefaultBehavior", typeof(AutoPopulateRegionBehavior));
        }

        protected override IRegion CreateRegion()
        {
            return base.CreateRegion();
        }
    }
}
