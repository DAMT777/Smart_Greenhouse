using System;
using SmartGreenhouse.Application.Services;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Rules;
using SmartGreenhouse.Repository;

// --- Wire up dependencies ---
var simulador = new SimuladorSensor(45f);
var sensorTemp = new SensorTemperatura("temp-01", "Celsius");
var bomba = new BombaAgua("bomba-01");
var ventilador = new Ventilador("vent-01");
var repo = new InMemoryGreenhouseRepo();
var regla = new ReglaRiego(umbralHumedadMinima: 50f, duracionRiegoRecomendada: 30, horasEntreRiegos: 6);

var monitoreo = new SensorMonitoringService(simulador, sensorTemp);
var riego = new IrrigationService(repo, bomba, regla);
var ventilacion = new VentilationService(ventilador, umbralTemp: 30f);
var controller = new GreenhouseController(monitoreo, riego, ventilacion, regla);

controller.iniciarSistema();

// --- Menu loop ---
Console.WriteLine("=== Smart Greenhouse ===");
Console.WriteLine("Comandos: READ | IRRIGATE ON | IRRIGATE OFF | IRRIGATE AUTO | SET MOISTURE_THRESHOLD <val> | SET TEMP_THRESHOLD <val> | EXIT");
Console.WriteLine();

while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();

    if (input == null)
        break;

    if (input.Trim().ToUpperInvariant() == "EXIT")
    {
        controller.detenerSistema();
        break;
    }

    controller.procesarComando(input);
}
