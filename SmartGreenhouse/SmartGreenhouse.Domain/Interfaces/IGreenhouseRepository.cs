using System.Collections.Generic;
using SmartGreenhouse.Domain.Entities;

namespace SmartGreenhouse.Domain.Interfaces
{
    public interface IGreenhouseRepository
    {
        void guardarLecturaHumedad(float valor);
        void registrarEvento(IrrigationEvent evento);
        List<IrrigationEvent> obtenerHistorial();
    }
}
