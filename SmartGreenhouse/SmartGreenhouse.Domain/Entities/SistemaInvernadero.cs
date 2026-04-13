namespace SmartGreenhouse.Domain.Entities
{
    public class SistemaInvernadero
    {
        public float umbralHumedad { get; set; }
        public float umbralTemperatura { get; set; }
        public string modo { get; set; }
        public string nombre { get; set; }

        public SistemaInvernadero(string nombre, float umbralHumedad, float umbralTemperatura, string modo)
        {
            this.nombre = nombre;
            this.umbralHumedad = umbralHumedad;
            this.umbralTemperatura = umbralTemperatura;
            this.modo = modo;
        }

        public string getNombre()
        {
            return nombre;
        }
    }
}
