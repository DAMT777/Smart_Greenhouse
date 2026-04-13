using System;

namespace SmartGreenhouse.Domain.Entities
{
    public class IrrigationEvent
    {
        public int duracionSeg { get; set; }
        public string causa { get; set; }
        public DateTime timestamp { get; set; }
        public float humedadAntes { get; set; }
        public float humedadDespues { get; set; }

        public IrrigationEvent(int duracionSeg, string causa, DateTime timestamp, float humedadAntes, float humedadDespues)
        {
            this.duracionSeg = duracionSeg;
            this.causa = causa;
            this.timestamp = timestamp;
            this.humedadAntes = humedadAntes;
            this.humedadDespues = humedadDespues;
        }

        public bool esExitoso()
        {
            return humedadDespues > humedadAntes;
        }
    }
}
