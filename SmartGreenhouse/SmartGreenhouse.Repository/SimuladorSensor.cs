using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Repository
{
    public class SimuladorSensor : ISensorHumedad
    {
        public float ValorSimulado { get; set; }

        public SimuladorSensor(float valorSimulado)
        {
            this.ValorSimulado = valorSimulado;
        }

        public float leerValor()
        {
            return ValorSimulado;
        }
    }
}
