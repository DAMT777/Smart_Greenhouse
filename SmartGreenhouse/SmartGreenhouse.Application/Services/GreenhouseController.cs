using System;
using SmartGreenhouse.Domain.Rules;

namespace SmartGreenhouse.Application.Services
{
    public class GreenhouseController
    {
        private SensorMonitoringService monitoreaSensores;
        private IrrigationService servicioRiego;
        private VentilationService ventilacionSvc;
        private ReglaRiego regla;

        public GreenhouseController(
            SensorMonitoringService monitoreaSensores,
            IrrigationService servicioRiego,
            VentilationService ventilacionSvc,
            ReglaRiego regla)
        {
            this.monitoreaSensores = monitoreaSensores;
            this.servicioRiego = servicioRiego;
            this.ventilacionSvc = ventilacionSvc;
            this.regla = regla;
        }

        public void ejecutarCicloMonitoreo()
        {
            var state = monitoreaSensores.obtenerEstadoActual();
            servicioRiego.evaluarYEjecutar(state);
            ventilacionSvc.evaluarTemperatura(state);
        }

        public void iniciarSistema()
        {
            Console.WriteLine("Sistema iniciado.");
        }

        public void detenerSistema()
        {
            servicioRiego.detenerRiego();
            ventilacionSvc.detenerVentilacion();
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
                    var state = monitoreaSensores.obtenerEstadoActual();
                    Console.WriteLine($"Humedad: {state.Humedad}% | Temperatura: {state.Temperatura}° | Modo: {state.ModoActual}");
                    break;

                case "IRRIGATE":
                    if (parts.Length > 1)
                    {
                        var sub = parts[1].ToUpperInvariant();
                        if (sub == "ON")
                            servicioRiego.forzarRiegoManual(30);
                        else if (sub == "OFF")
                            servicioRiego.detenerRiego();
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
                                regla.actualizarUmbral(value);
                                Console.WriteLine($"Umbral de humedad actualizado a {value}%");
                            }
                            else if (target == "TEMP_THRESHOLD")
                            {
                                ventilacionSvc.actualizarUmbralTemp(value);
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
