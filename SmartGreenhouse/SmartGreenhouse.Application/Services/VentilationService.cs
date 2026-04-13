using SmartGreenhouse.Domain.Entities;

namespace SmartGreenhouse.Application.Services
{
    public class VentilationService
    {
        private Ventilador ventilador;
        private float umbralTemp;

        public VentilationService(Ventilador ventilador, float umbralTemp)
        {
            this.ventilador = ventilador;
            this.umbralTemp = umbralTemp;
        }

        public void actualizarUmbralTemp(float valor)
        {
            umbralTemp = valor;
        }

        public void evaluarTemperatura(ClimateState state)
        {
            if (state.Temperatura > umbralTemp)
            {
                ventilador.encender();
                ventilador.setVelocidad(100);
            }
            else
            {
                ventilador.apagar();
                ventilador.setVelocidad(0);
            }
        }

        public void forzarVentilacion(int nivel)
        {
            ventilador.encender();
            ventilador.setVelocidad(nivel);
        }

        public void detenerVentilacion()
        {
            ventilador.apagar();
            ventilador.setVelocidad(0);
        }
    }
}
