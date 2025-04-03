using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Tatweer.SafeCity.Localization
{
    public static class SafeCityLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(SafeCityConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(SafeCityLocalizationConfigurer).GetAssembly(),
                        "Tatweer.SafeCity.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
