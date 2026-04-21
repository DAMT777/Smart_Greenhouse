using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Domain.Entities;

public class SensorTemperatura : Sensor, ISensorTemperatura
{
    private static readonly Random _random = new();

    public int PinArduino { get; set; }

    public string Unidad { get; set; } = string.Empty;

    public override float LeerValor()
    {
        UltimaLectura = 15f + (float)(_random.NextDouble() * 25.0);
        return UltimaLectura;
    }

    public float LeerEnCelsius()
    {
        return LeerValor();
    }
}
