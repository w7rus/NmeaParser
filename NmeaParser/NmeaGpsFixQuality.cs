namespace NmeaParser;

public enum NmeaGpsFixQuality
{
    Invalid = 0,
    SinglePoint = 1,
    PseudorangeDifferential = 2,
    RtkFix = 4,
    RtkFloat = 5,
    DeadReckoningMode = 6,
    ManualInputMode = 7,
    SimulationMode = 8,
    Waas = 9
}