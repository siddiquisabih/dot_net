using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;
using Tatweer.ITSM;

namespace Tatweer.RadarManagment.Localization
{
    public static class ITSMLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(ITSMConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ITSMLocalizationConfigurer).GetAssembly(),
                        "Tatweer.ITSM.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
