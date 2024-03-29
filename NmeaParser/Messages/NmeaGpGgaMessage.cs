using NmeaParser.Messages.Base;

namespace NmeaParser.Messages;

public record NmeaGpGgaMessage : NmeaMessage
{
    public TimeOnly UtcTime { get; private set; }

    public double Latitude { get; private set; }

    public double Longitude { get; private set; }

    public NmeaGpsFixQuality FixQuality { get; private set; }

    public int NumberOfSatellites { get; private set; }

    public float Hdop { get; private set; }

    public float Altitude { get; private set; }

    public string AltitudeUnits { get; private set; }

    public float HeightOfGeoid { get; private set; }

    public string HeightOfGeoidUnits { get; private set; }

    public TimeSpan AgeOfCorrection { get; private set; }

    public int? DifferentialBaseStationId { get; private set; }

    public string Message { get; private set; }

    public override void Parse(string[] messageParts)
    {
        if (messageParts is { Length: > 15 })
            throw new ArgumentException("Invalid GPGGA message");

        UtcTime = NmeaExtensions.ToTimeOnly(messageParts[1]);
        Latitude = messageParts[2].ToCoordinates(messageParts[3], NmeaCoordinate.Latitude);
        Longitude = messageParts[4].ToCoordinates(messageParts[5], NmeaCoordinate.Longitude);
        FixQuality = (NmeaGpsFixQuality)Enum.Parse(typeof(NmeaGpsFixQuality), messageParts[6]);
        NumberOfSatellites = messageParts[7].ToInteger();
        Hdop = messageParts[8].ToFloat();
        Altitude = messageParts[9].ToFloat();
        AltitudeUnits = messageParts[10];
        HeightOfGeoid = messageParts[11].ToFloat();
        HeightOfGeoidUnits = messageParts[12];
        AgeOfCorrection = TimeSpan.FromSeconds(messageParts[13].ToDouble());
        DifferentialBaseStationId = messageParts[14].ToInteger();
        Message = string.Join(',', messageParts);
    }
}