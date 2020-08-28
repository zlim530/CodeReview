using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace LearningAbpDemo.Localization
{
    public static class LearningAbpDemoLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(LearningAbpDemoConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(LearningAbpDemoLocalizationConfigurer).GetAssembly(),
                        "LearningAbpDemo.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
