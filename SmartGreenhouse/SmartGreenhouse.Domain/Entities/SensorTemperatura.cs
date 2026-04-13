namespace SmartGreenhouse.Domain.Entities
{
    public class SensorTemperatura : Sensor
    {
        public string unidad { get; set; }

        public SensorTemperatura(string id, string unidad)
        {
            this.id = id;
            this.tipo = "Temperatura";
            this.unidad = unidad;
        }

        public override float leerValor()
        {
            return 0f;
        }
    }
}
