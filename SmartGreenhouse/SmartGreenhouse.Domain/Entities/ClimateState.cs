using System;

namespace SmartGreenhouse.Domain.Entities
{
    public class ClimateState
    {
        public float Humedad { get; set; }
        public float Temperatura { get; set; }
        public DateTime Timestamp { get; set; }
        public string ModoActual { get; set; }

        public ClimateState(float humedad, float temperatura, DateTime timestamp, string modoActual)
        {
            Humedad = humedad;
            Temperatura = temperatura;
            Timestamp = timestamp;
            ModoActual = modoActual;
        }

        public bool esValido()
        {
            if (Humedad >= 0 && Humedad <= 100 && Temperatura >= -50 && Temperatura <= 50)
                return true;
            return false;
        }
    }
}
