using SmartGreenhouse.Domain.Entities;

namespace SmartGreenhouse.Application.Services
{
    public class VentilationService
    {
        private Ventilador _ventilador;
        private float _umbralTemp;

        public VentilationService(Ventilador ventilador, float umbralTemp)
        {
            _ventilador = ventilador;
            _umbralTemp = umbralTemp;
        }

        public void actualizarUmbralTemp(float valor)
        {
            _umbralTemp = valor;
        }

        public void evaluarTemperatura(ClimateState state)
        {
            if (state.Temperatura > _umbralTemp)
            {
                _ventilador.encender();
                _ventilador.setVelocidad(100);
            }
            else
            {
                _ventilador.apagar();
                _ventilador.setVelocidad(0);
            }
        }

        public void forzarVentilacion(int nivel)
        {
            _ventilador.encender();
            _ventilador.setVelocidad(nivel);
        }

        public void detenerVentilacion()
        {
            _ventilador.apagar();
            _ventilador.setVelocidad(0);
        }
    }
}
