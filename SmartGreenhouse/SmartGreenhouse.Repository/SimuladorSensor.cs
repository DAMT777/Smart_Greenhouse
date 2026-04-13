using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Repository
{
    public class SimuladorSensor : ISensorHumedad
    {
        public float valorSimulado { get; set; }

        public SimuladorSensor(float valorSimulado)
        {
            this.valorSimulado = valorSimulado;
        }

        public float leerValor()
        {
            return valorSimulado;
        }
    }
}
