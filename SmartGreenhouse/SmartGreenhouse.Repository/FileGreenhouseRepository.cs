using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Repository;

public class FileGreenhouseRepository : IGreenhouseRepository
{
    private List<float> _historialLecturas = new();
    private List<string> _historialRiegos = new();
    private List<IrrigationEvent> _historialEventos = new();

    private string _readingsPath = Path.Combine(AppContext.BaseDirectory, "readings.json");
    private string _eventsPath = Path.Combine(AppContext.BaseDirectory, "irrigation_events.json");

    public void GuardarLecturaHumedad(float valor)
    {
        _historialLecturas.Add(valor);
        string json = JsonSerializer.Serialize(_historialLecturas, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_readingsPath, json);
    }

    public void RegistrarEvento(IrrigationEvent evento)
    {
        _historialEventos.Add(evento);
        _historialRiegos.Add($"{evento.Timestamp:O}|{evento.Causa}|{evento.DuracionSeg}");

        string json = JsonSerializer.Serialize(_historialEventos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_eventsPath, json);
    }

    public List<IrrigationEvent> ObtenerHistorial()
    {
        if (!File.Exists(_eventsPath))
        {
            return new List<IrrigationEvent>();
        }

        string json = File.ReadAllText(_eventsPath);
        List<IrrigationEvent>? eventos = JsonSerializer.Deserialize<List<IrrigationEvent>>(json);
        return eventos ?? new List<IrrigationEvent>();
    }
}
