using System.Collections.Generic;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Repository
{
    public class InMemoryGreenhouseRepo : IGreenhouseRepository
    {
        private List<float> _historialLecturas = new List<float>();
        private List<IrrigationEvent> _historialEventos = new List<IrrigationEvent>();

        public void guardarLecturaHumedad(float valor)
        {
            _historialLecturas.Add(valor);
        }

        public void registrarEvento(IrrigationEvent evento)
        {
            _historialEventos.Add(evento);
        }

        public List<IrrigationEvent> obtenerHistorial()
        {
            return _historialEventos;
        }
    }
}
