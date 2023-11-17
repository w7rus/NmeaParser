using NmeaParser.Messages.Base;

namespace NmeaParser.Messages;

public record NmeaGpHdtMessage : NmeaMessage
{
    public double Heading { get; private set; }
    public bool DegreesTrue { get; private set; }

    public string Message { get; private set; }

    public override void Parse(string[] messageParts)
    {
        if (messageParts is { Length: > 3 })
            throw new ArgumentException("Invalid GPHDT message");

        Heading = messageParts[1].ToDouble();
        DegreesTrue = messageParts[2].ToBoolean("T");
        Message = string.Join(',', messageParts);
    }
}