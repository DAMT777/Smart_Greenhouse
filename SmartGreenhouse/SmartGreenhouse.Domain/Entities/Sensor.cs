namespace SmartGreenhouse.Domain.Entities
{
    public abstract class Sensor
    {
        public string Id { get; set; }
        public float UltimaLectura { get; set; }
        public string Tipo { get; set; }

        public abstract float leerValor();
    }
}
