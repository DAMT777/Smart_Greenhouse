namespace SmartGreenhouse.Domain.Entities
{
    public abstract class Sensor
    {
        public string id { get; set; }
        public float ultimaLectura { get; set; }
        public string tipo { get; set; }

        public abstract float leerValor();
    }
}
