using MagicLoaderGenerator.Localization.Providers;
using MagicLoaderGenerator.Filesystem.Generators;
using Microsoft.Extensions.Configuration;
using MarkQuestDungeonsML;

var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("config.json", optional: false);

IConfiguration config = builder.Build();

var appConfig = new MarkQuestDungeonsConfig(config);
var mod = new MagicLoaderMod(new JsonLocalizationProvider(appConfig), appConfig);

mod.Generate(new ZipOutputGenerator(appConfig), true);