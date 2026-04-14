using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Domain.Entities
{
    public class SensorHumedadSuelo : Sensor, ISensorHumedad
    {
        public float Calibracion { get; set; }

        public SensorHumedadSuelo(string id, float calibracion)
        {
            this.Id = id;
            this.Tipo = "HumedadSuelo";
            this.Calibracion = calibracion;
        }

        public override float leerValor()
        {
            return 0f;
        }
    }
}
