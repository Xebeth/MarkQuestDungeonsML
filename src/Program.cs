using Microsoft.Extensions.Configuration;
using MarkQuestDungeonsML;

var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("config.json", optional: false);

IConfiguration config = builder.Build();

var generator = new MagicLoaderGenerator(config);

generator.LoadLocalization().Generate();