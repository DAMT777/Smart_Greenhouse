using System.Collections.Generic;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Repository
{
    public class InMemoryGreenhouseRepo : IGreenhouseRepository
    {
        private List<float> historialLecturas = new List<float>();
        private List<IrrigationEvent> historialEventos = new List<IrrigationEvent>();

        public void guardarLecturaHumedad(float valor)
        {
            historialLecturas.Add(valor);
        }

        public void registrarEvento(IrrigationEvent evento)
        {
            historialEventos.Add(evento);
        }

        public List<IrrigationEvent> obtenerHistorial()
        {
            return historialEventos;
        }
    }
}
