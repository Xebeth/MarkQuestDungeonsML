using Microsoft.Extensions.Configuration;
using MagicLoaderGenerator.Filesystem;

namespace MarkQuestDungeonsML;

public record MarkQuestDungeonsConfig: AppConfig
{
    private static Dictionary<string, string> DefaultSuffixes => new() {
        { "angled",      "<!>"  },
        { "asterisk",    "*"    },
        { "braces",      "{!}"  },
        { "brackets",    "[!]"  },
        { "degrees",     "°"    },
        { "parentheses", "(!)"  },
        { "sharp",       "#"    }
    };
    
    public MarkQuestDungeonsConfig(IConfiguration config) : base(config) {}

    public Dictionary<string, string> Suffixes { get; } = DefaultSuffixes;
}