using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Domain.Entities
{
    public class SensorHumedadSuelo : Sensor, ISensorHumedad
    {
        public float calibracion { get; set; }

        public SensorHumedadSuelo(string id, float calibracion)
        {
            this.id = id;
            this.tipo = "HumedadSuelo";
            this.calibracion = calibracion;
        }

        public override float leerValor()
        {
            return 0f;
        }
    }
}
