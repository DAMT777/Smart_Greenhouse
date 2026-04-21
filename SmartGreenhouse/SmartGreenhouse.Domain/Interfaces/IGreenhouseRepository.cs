using System.Collections.Generic;
using SmartGreenhouse.Domain.Entities;

namespace SmartGreenhouse.Domain.Interfaces;

public interface IGreenhouseRepository
{
    void GuardarLecturaHumedad(float valor);

    void RegistrarEvento(IrrigationEvent evento);

    List<IrrigationEvent> ObtenerHistorial();
}
