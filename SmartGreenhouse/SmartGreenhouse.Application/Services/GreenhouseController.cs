using System;
using SmartGreenhouse.Domain.Rules;

namespace SmartGreenhouse.Application.Services
{
    public class GreenhouseController
    {
        private SensorMonitoringService _monitoreaSensores;
        private IrrigationService _servicioRiego;
        private VentilationService _ventilacionSvc;
        private ReglaRiego _regla;

        public GreenhouseController(
            SensorMonitoringService monitoreaSensores,
            IrrigationService servicioRiego,
            VentilationService ventilacionSvc,
            ReglaRiego regla)
        {
            _monitoreaSensores = monitoreaSensores;
            _servicioRiego = servicioRiego;
            _ventilacionSvc = ventilacionSvc;
            _regla = regla;
        }

        public void ejecutarCicloMonitoreo()
        {
            var state = _monitoreaSensores.obtenerEstadoActual();
            _servicioRiego.evaluarYEjecutar(state);
            _ventilacionSvc.evaluarTemperatura(state);
        }

        public void iniciarSistema()
        {
            Console.WriteLine("Sistema iniciado.");
        }

        public void detenerSistema()
        {
            _servicioRiego.detenerRiego();
            _ventilacionSvc.detenerVentilacion();
            Console.WriteLine("Sistema detenido.");
        }

        public void procesarComando(string cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd))
                return;

            var parts = cmd.Trim().Split(' ');
            var command = parts[0].ToUpperInvariant();

            switch (command)
            {
                case "READ":
                    var state = _monitoreaSensores.obtenerEstadoActual();
                    Console.WriteLine($"Humedad: {state.Humedad}% | Temperatura: {state.Temperatura}° | Modo: {state.ModoActual}");
                    break;

                case "IRRIGATE":
                    if (parts.Length > 1)
                    {
                        var sub = parts[1].ToUpperInvariant();
                        if (sub == "ON")
                            _servicioRiego.forzarRiegoManual(30);
                        else if (sub == "OFF")
                            _servicioRiego.detenerRiego();
                        else if (sub == "AUTO")
                            ejecutarCicloMonitoreo();
                    }
                    break;

                case "SET":
                    if (parts.Length >= 3)
                    {
                        var target = parts[1].ToUpperInvariant();
                        if (float.TryParse(parts[2], out float value))
                        {
                            if (target == "MOISTURE_THRESHOLD")
                            {
                                _regla.actualizarUmbral(value);
                                Console.WriteLine($"Umbral de humedad actualizado a {value}%");
                            }
                            else if (target == "TEMP_THRESHOLD")
                            {
                                _ventilacionSvc.actualizarUmbralTemp(value);
                                Console.WriteLine($"Umbral de temperatura actualizado a {value}°");
                            }
                        }
                    }
                    break;

                default:
                    Console.WriteLine($"Comando desconocido: {cmd}");
                    break;
            }
        }
    }
}
