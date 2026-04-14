using System;

namespace SmartGreenhouse.Domain.Entities
{
    public class IrrigationEvent
    {
        public int DuracionSeg { get; set; }
        public string Causa { get; set; }
        public DateTime Timestamp { get; set; }
        public float HumedadAntes { get; set; }
        public float HumedadDespues { get; set; }

        public IrrigationEvent(int duracionSeg, string causa, DateTime timestamp, float humedadAntes, float humedadDespues)
        {
            this.DuracionSeg = duracionSeg;
            this.Causa = causa;
            this.Timestamp = timestamp;
            this.HumedadAntes = humedadAntes;
            this.HumedadDespues = humedadDespues;
        }

        public bool esExitoso()
        {
            return HumedadDespues > HumedadAntes;
        }
    }
}
