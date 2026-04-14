namespace SmartGreenhouse.Domain.Entities
{
    public class SistemaInvernadero
    {
        public float UmbralHumedad { get; set; }
        public float UmbralTemperatura { get; set; }
        public string Modo { get; set; }
        public string Nombre { get; set; }

        public SistemaInvernadero(string nombre, float umbralHumedad, float umbralTemperatura, string modo)
        {
            this.Nombre = nombre;
            this.UmbralHumedad = umbralHumedad;
            this.UmbralTemperatura = umbralTemperatura;
            this.Modo = modo;
        }

        public string getNombre()
        {
            return Nombre;
        }
    }
}
