using System;

namespace SmartGreenhouse.Domain.Entities;

public class ClimateState
{
    public float Humedad { get; set; }

    public float Temperatura { get; set; }

    public DateTime Timestamp { get; set; }

    public string ModoActual { get; set; } = string.Empty;

    public bool EsValido()
    {
        return Humedad >= 0 && Humedad <= 100 && Temperatura >= -50 && Temperatura <= 100;
    }
}
