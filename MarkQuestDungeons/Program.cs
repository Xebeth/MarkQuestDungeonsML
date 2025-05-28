using MagicLoaderGenerator.Filesystem.Abstractions;
using MagicLoaderGenerator.Localization.Transforms;
using MagicLoaderGenerator.Filesystem.Generators;
using Microsoft.Extensions.Configuration;
using MagicLoaderGenerator;
using MarkQuestDungeons;

var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("config.json", optional: false);
var variants = new Dictionary<string, IMagicLoaderFileTransform?>();
var appConfig = new MarkQuestDungeonsConfig(builder.Build());
var mod = new MagicLoaderMod(appConfig);

foreach (var (variant, prefix) in appConfig.Suffixes)
{
    variants.Add(variant, new ValueSuffixTransform(prefix));
}

var outputDir = mod.Generate(new ZipOutputGenerator(appConfig), variants);

#if DEBUG
System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo {
    UseShellExecute = true,
    FileName = outputDir,
    Verb = "open"
});
#endif
