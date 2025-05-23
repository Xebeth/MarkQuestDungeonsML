using MagicLoaderGenerator.Localization.Abstractions;
using MagicLoaderGenerator.Localization.Transforms;
using MagicLoaderGenerator.Filesystem.Abstractions;

namespace MarkQuestDungeonsML;

public class MagicLoaderMod(ILocalizationProvider localization, MarkQuestDungeonsConfig config)
{
    public void Generate(IModOutputGenerator outputGenerator, bool cleanOutput)
    {
        var outputDir = Path.Combine(config.OutputDirectory, config.ModName);
        
        if (cleanOutput && Directory.Exists(outputDir))
        {
            Directory.Delete(outputDir, true);
        }

        foreach (var language in localization.SupportedLanguages)
        {
            foreach (var (name, suffix) in config.Suffixes)
            {
                var fileTransform = new ValueSuffixTransformer(localization, suffix);
                var outputName = $"{fileTransform.GetOutputName(config.ModName, language)}_{name}";

                foreach (var (filename, magicLoaderFile) in config.ModFiles.Where(f => f.Value.HasEntries()))
                {
                    var transformedFile = fileTransform.Transform(language, magicLoaderFile);

                    outputGenerator.AddFile(filename, transformedFile);
                }

                outputGenerator.Output(outputName);
            }
        }
    }
}
