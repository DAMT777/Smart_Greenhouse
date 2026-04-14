using System;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Application.Services
{
    public class SensorMonitoringService
    {
        private ISensorHumedad _sensorSuelo;
        private SensorTemperatura _sensorTemp;

        public SensorMonitoringService(ISensorHumedad sensorSuelo, SensorTemperatura sensorTemp)
        {
            _sensorSuelo = sensorSuelo;
            _sensorTemp = sensorTemp;
        }

        public ClimateState obtenerEstadoActual()
        {
            float humedad = _sensorSuelo.leerValor();
            float temperatura = _sensorTemp.leerValor();
            return new ClimateState(humedad, temperatura, DateTime.Now, "auto");
        }

        public bool validarLectura(float valor)
        {
            return valor >= 0 && valor <= 100;
        }
    }
}
