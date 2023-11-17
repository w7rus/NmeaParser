namespace NmeaParser.Messages.Base;

public abstract record NmeaMessage
{
    public abstract void Parse(string[] messageParts);
}