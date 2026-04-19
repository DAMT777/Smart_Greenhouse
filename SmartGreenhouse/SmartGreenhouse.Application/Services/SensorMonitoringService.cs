using System;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Application.Services;

public class SensorMonitoringService
{
    private ISensorHumedad _sensorSuelo;
    private ISensorTemperatura _sensorTemp;

    public SensorMonitoringService(ISensorHumedad sensorSuelo, ISensorTemperatura sensorTemp)
    {
        _sensorSuelo = sensorSuelo;
        _sensorTemp = sensorTemp;
    }

    public ClimateState ObtenerEstadoActual()
    {
        float humedad = _sensorSuelo.LeerValor();
        float temperatura = _sensorTemp.LeerValor();

        return new ClimateState
        {
            Humedad = humedad,
            Temperatura = temperatura,
            Timestamp = DateTime.Now,
            ModoActual = "AUTO"
        };
    }

    public bool ValidarLectura(float valor)
    {
        return valor >= 0 && valor <= 100;
    }
}
