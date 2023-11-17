using NmeaParser.Messages.Base;

namespace NmeaParser.Messages;

public record NmeaGpZdaMessage : NmeaMessage
{
    public DateTimeOffset Utc { get; private set; }

    public string Message { get; private set; }

    public override void Parse(string[] messageParts)
    {
        if (messageParts is { Length: > 7 })
            throw new ArgumentException("Invalid GPZDA message");

        var timeOnly = NmeaExtensions.ToTimeOnly(messageParts[1]);
        var dateOnly = NmeaExtensions.ToDateOnly(messageParts[1], messageParts[2], messageParts[3]);

        Utc = dateOnly.ToDateTime(timeOnly, DateTimeKind.Utc);
        Message = string.Join(',', messageParts);
    }
}