using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Domain.Entities;

public abstract class Sensor : ISensor
{
    public string Id { get; set; } = string.Empty;

    public float UltimaLectura { get; set; }

    public abstract float LeerValor();

    public string GetId()
    {
        return Id;
    }
}
