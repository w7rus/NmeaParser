using NmeaParser.Messages;
using NmeaParser.Messages.Base;

namespace NmeaParser;

public sealed class NmeaParser
{
    private sealed class NmeaParserNmeaMessageTypeNotFoundException : Exception
    {
    }

    private sealed class NmeaParserNmeaMessageTypeFailedToCreateInstance : Exception
    {
    }

    private sealed class NmeaParserNotANmeaMessage : Exception
    {
    }

    public Dictionary<string, Type> TypeDictionary { get; } = new()
    {
        {
            "GPGGA",
            typeof(NmeaGpGgaMessage)
        },
        {
            "GPHDT",
            typeof(NmeaGpHdtMessage)
        },
        {
            "GPZDA",
            typeof(NmeaGpZdaMessage)
        }
    };

    public NmeaMessage Parse(string data)
    {
        if (!data.StartsWith("$")) throw new NmeaParserNotANmeaMessage();
        var messageParts = data.RemoveAfter("*").Split(',');
        if (!TypeDictionary.TryGetValue(messageParts[0].TrimStart('$'), out var type)) throw new NmeaParserNmeaMessageTypeNotFoundException();
        var nmeaMessage = (NmeaMessage)(Activator.CreateInstance(type) ?? throw new NmeaParserNmeaMessageTypeFailedToCreateInstance());
        nmeaMessage.Parse(messageParts);
        return nmeaMessage;
    }
}