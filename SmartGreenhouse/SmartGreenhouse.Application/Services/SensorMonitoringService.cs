using System;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Application.Services
{
    public class SensorMonitoringService
    {
        private ISensorHumedad sensorSuelo;
        private SensorTemperatura sensorTemp;

        public SensorMonitoringService(ISensorHumedad sensorSuelo, SensorTemperatura sensorTemp)
        {
            this.sensorSuelo = sensorSuelo;
            this.sensorTemp = sensorTemp;
        }

        public ClimateState obtenerEstadoActual()
        {
            float humedad = sensorSuelo.leerValor();
            float temperatura = sensorTemp.leerValor();
            return new ClimateState(humedad, temperatura, DateTime.Now, "auto");
        }

        public bool validarLectura(float valor)
        {
            return valor >= 0 && valor <= 100;
        }
    }
}
