using System;

namespace SmartGreenhouse.Domain.Entities;

public class IrrigationEvent
{
    public int DuracionSeg { get; set; }

    public string Causa { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    public float HumedadAntes { get; set; }

    public float HumedadDespues { get; set; }

    public bool EsExitoso()
    {
        return HumedadDespues > HumedadAntes;
    }
}
