namespace SmartGreenhouse.Domain.Entities
{
    public class SensorTemperatura : Sensor
    {
        public string Unidad { get; set; }

        public SensorTemperatura(string id, string unidad)
        {
            this.Id = id;
            this.Tipo = "Temperatura";
            this.Unidad = unidad;
        }

        public override float leerValor()
        {
            return 0f;
        }
    }
}
