using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Domain.Entities;

public class SensorHumedadSuelo : Sensor, ISensorHumedad
{
    private static readonly Random _random = new();

    public int PinArduino { get; set; }

    public override float LeerValor()
    {
        UltimaLectura = (float)(_random.NextDouble() * 100.0);
        return UltimaLectura;
    }

    public void Calibrar()
    {
        Console.WriteLine($"Sensor de humedad {Id} calibrado en pin {PinArduino}.");
    }
}
