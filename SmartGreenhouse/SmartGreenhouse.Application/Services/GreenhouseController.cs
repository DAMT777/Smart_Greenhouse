using System;

namespace SmartGreenhouse.Application.Services
{
    public class GreenhouseController
    {
        private SensorMonitoringService monitoreaSensores;
        private IrrigationService servicioRiego;
        private VentilationService ventilacionSvc;

        public GreenhouseController(
            SensorMonitoringService monitoreaSensores,
            IrrigationService servicioRiego,
            VentilationService ventilacionSvc)
        {
            this.monitoreaSensores = monitoreaSensores;
            this.servicioRiego = servicioRiego;
            this.ventilacionSvc = ventilacionSvc;
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
                    Console.WriteLine($"Humedad: {state._humedad}% | Temperatura: {state._temperatura}° | Modo: {state._modoActual}");
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
                                Console.WriteLine($"Umbral de humedad actualizado a {value}%");
                            else if (target == "TEMP_THRESHOLD")
                                Console.WriteLine($"Umbral de temperatura actualizado a {value}°");
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
