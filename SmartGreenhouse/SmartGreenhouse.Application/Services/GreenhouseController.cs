using System;

namespace SmartGreenhouse.Application.Services;

public class GreenhouseController
{
    private SensorMonitoringService _monitoreoSensores;
    private IrrigationService _servicioRiego;
    private VentilationService _ventilacionSvc;

    public GreenhouseController(
        SensorMonitoringService monitoreoSensores,
        IrrigationService servicioRiego,
        VentilationService ventilacionSvc)
    {
        _monitoreoSensores = monitoreoSensores;
        _servicioRiego = servicioRiego;
        _ventilacionSvc = ventilacionSvc;
    }

    public void EjecutarCicloMonitoreo()
    {
        var state = _monitoreoSensores.ObtenerEstadoActual();
        _servicioRiego.EvaluarYEjecutar(state);
        _ventilacionSvc.EvaluarTemperatura(state);
        Console.WriteLine($"Estado -> Humedad: {state.Humedad:F2}% | Temperatura: {state.Temperatura:F2} C | Modo: {state.ModoActual}");
    }

    public void IniciarSistema()
    {
        Console.WriteLine("Sistema SmartGreenhouse iniciado.");
    }

    public void DetenerSistema()
    {
        _servicioRiego.DetenerRiego();
        _ventilacionSvc.DetenerVentilacion();
        Console.WriteLine("Sistema SmartGreenhouse detenido.");
    }
}
