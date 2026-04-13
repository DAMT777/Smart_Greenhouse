using System;

namespace SmartGreenhouse.Domain.Entities
{
    public class ClimateState
    {
        public float _humedad { get; set; }
        public float _temperatura { get; set; }
        public DateTime _timeStamp { get; set; }
        public string _modoActual { get; set; }

        public ClimateState(float humedad, float temperatura, DateTime timestamp, string modoActual)
        {
            _humedad = humedad;
            _temperatura = temperatura;
            _timeStamp = timestamp;
            _modoActual = modoActual;
        }

        public bool esValido()
        {
            if (_humedad >= 0 && _humedad <= 100 && _temperatura >= -50 && _temperatura <= 50)
                return true;
            return false;
        }
    }
}
