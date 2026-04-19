using System.Collections.Generic;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Repository;

public class InMemoryGreenhouseRepo : IGreenhouseRepository
{
    private List<float> _historialLecturas = new();
    private List<string> _historialRiegos = new();
    private List<IrrigationEvent> _historialEventos = new();

    public void GuardarLecturaHumedad(float valor)
    {
        _historialLecturas.Add(valor);
        Console.WriteLine($"Lectura guardada en memoria: {valor:F2}%");
    }

    public void RegistrarEvento(IrrigationEvent evento)
    {
        _historialEventos.Add(evento);
        _historialRiegos.Add($"{evento.Timestamp}: {evento.Causa} - {evento.DuracionSeg}s");
        Console.WriteLine($"Evento registrado en memoria: {evento.Causa} ({evento.DuracionSeg}s)");
    }

    public List<IrrigationEvent> ObtenerHistorial()
    {
        return _historialEventos;
    }
}
