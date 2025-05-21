using Microsoft.Extensions.Configuration;
using System.Text.Encodings.Web;
using System.IO.Compression;
using System.Text.Json;
using System.Text;

namespace MarkQuestDungeonsML;

public class MagicLoaderGenerator
{
    private readonly AppConfig _appConfig = new();

    public MagicLoaderGenerator(IConfiguration config)
    {
        config.Bind(_appConfig);
    }

    private readonly Dictionary<string, Dictionary<string, string>> _localizationStrings = new();

    public MagicLoaderGenerator LoadLocalization()
    {
        var locStrings = _appConfig.LocalizationStrings.Count > 0 ? _appConfig.LocalizationStrings : throw new InvalidOperationException("Localization strings not set");
        var localizationDir = _appConfig.LocalizationDirectory ?? throw new InvalidOperationException("Localization directory not set");
        var languages = _appConfig.Languages ?? throw new InvalidOperationException("Languages not set");

        foreach (var language in languages)
        {
            if (Directory.Exists($"{localizationDir}/{language}"))
            {
                var jsonFile = $"{localizationDir}/{language}/Game.json";
                var builder = new ConfigurationBuilder().AddJsonFile(jsonFile);
                var localizationFile = new LocalizationFile();
                var localizationConfig = builder.Build();

                localizationConfig.Bind(localizationFile);

                if (localizationFile.ST_FullNames != null)
                {
                    _localizationStrings[language] = localizationFile.ST_FullNames.Where(fn => locStrings.Contains(fn.Key, StringComparer.OrdinalIgnoreCase))
                                                                                  .ToDictionary(fn => fn.Key, fn => fn.Value);
                }
            }
        }

        return this;
    }

    public MagicLoaderGenerator Generate()
    {
        if (_localizationStrings.Count == 0)
            return this;

        var locStrings = _appConfig.LocalizationStrings.Count > 0 ? _appConfig.LocalizationStrings : throw new InvalidOperationException("Localization strings not set");
        var modDirectoryStructure = _appConfig.ModDirectoryStructure ?? throw new InvalidOperationException("Mod directory structure not set");
        var suffixes = _appConfig.Suffixes.Count > 0 ? _appConfig.Suffixes : throw new InvalidOperationException("Suffixes not set");
        var outputDirectory = _appConfig.OutputDirectory ?? throw new InvalidOperationException("Output directory not set");
        var languages = _appConfig.Languages ?? throw new InvalidOperationException("Languages not set");
        var options = new JsonSerializerOptions {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        if (Directory.Exists(outputDirectory))
            Directory.Delete(outputDirectory, true);

        Directory.CreateDirectory(outputDirectory);

        foreach (var language in languages)
        {
            foreach (var suffix in suffixes)
            {
                var zipName = $"{_appConfig.OutputDirectory}/MarkQuestDungeonsML_{language}_{suffix.Key}.zip";
                var lines = new Dictionary<string, string>();

                foreach (var locString in locStrings)
                {
                    var value = _localizationStrings[language].GetValueOrDefault(locString, $"$[[{locString}]]");
                    
                    lines[locString] = $"{value} {suffix.Value}";
                }
                
                var jsonData = JsonSerializer.Serialize(new MagicLoaderFile(lines), options);
                
                var zipContent = new MemoryStream();
                using var archive = new ZipArchive(zipContent, ZipArchiveMode.Create);

                AddEntry(modDirectoryStructure, Encoding.UTF8.GetBytes(jsonData), archive);
                File.WriteAllBytes(zipName, zipContent.ToArray());
            }
        }

        return this;
    }
    
    private static void AddEntry(string fileName, byte[] fileContent, ZipArchive archive)
    {
        var entry = archive.CreateEntry(fileName);
        using var stream = entry.Open();

        stream.Write(fileContent, 0, fileContent.Length);
    }
}