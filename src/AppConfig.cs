using Microsoft.Extensions.Configuration;

namespace MarkQuestDungeonsML;

public record AppConfig()
{
    public string OutputDirectory { get; set; } = Directory.GetCurrentDirectory();
    public Dictionary<string, string> Suffixes { get; set; } = DefaultSuffixes;
    public List<string> Languages { get; set; } = DefaultLanguages;

    public List<string> LocalizationStrings { get; set; } = [];
    public string LocalizationDirectory { get; set; } = null!;
    public string ModDirectoryStructure { get; set; } = null!;

    public AppConfig(IConfiguration config): this()
    {
        config.Bind(this);
    }

    private static Dictionary<string, string> DefaultSuffixes => new() {
        { "asterisk",    "*"    },
        { "braces",      "{!}"  },
        { "brackets",    "[!]"  },
        { "degrees",     "°"    },
        { "parentheses", "(!)"  },
        { "sharp",       "#"    }
    };

    private static List<string> DefaultLanguages => [
        "de",
        "es",
        "fr",
        "it",
        "ja",
        "pl",
        "pt",
        "zh-Hans"
    ];
}