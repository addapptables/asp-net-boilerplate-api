using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Localization.Sources;
using Abp.Reflection.Extensions;
using System.Reflection;

namespace Addapptables.Boilerplate.Localization
{
    public static class BoilerplateLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(BoilerplateConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(BoilerplateLocalizationConfigurer).GetAssembly(),
                        "Addapptables.Boilerplate.Localization.SourceFiles"
                    )
                )
            );
            ExtendAbp(localizationConfiguration);
            ExtendAbpWeb(localizationConfiguration);
        }

        public static void ExtendAbp(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Extensions.Add(
                new LocalizationSourceExtensionInfo("Abp",
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(BoilerplateLocalizationConfigurer).GetAssembly(),
                        "Addapptables.Boilerplate.Localization.AbpXmlSource"
                    )
                )
            );
        }

        public static void ExtendAbpWeb(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Extensions.Add(
                new LocalizationSourceExtensionInfo("AbpWeb",
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Addapptables.Boilerplate.Localization.AbpWebXmlSource"
                    )
                )
            );
        }
    }
}
