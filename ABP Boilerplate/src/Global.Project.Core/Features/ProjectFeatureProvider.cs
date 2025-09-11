using Abp.Application.Features;
using Abp.Localization;
using Global.Project;

namespace Global.Project.Features
{
    public class ProjectFeatureProvider : FeatureProvider
    {
        public override void SetFeatures(IFeatureDefinitionContext context)
        {

            context.Create(FeaturesName.DMS, defaultValue: "true", displayName: L("DMS"));
            context.Create(FeaturesName.Platform, defaultValue: "true", displayName: L("Platform"));
            context.Create(FeaturesName.Platform_Inventory, defaultValue: "true", displayName: L("Platform Inventory"));

        }


        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ProjectConsts.LocalizationSourceName);
        }

    }

}
