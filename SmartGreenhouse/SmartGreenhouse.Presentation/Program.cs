using System;
using SmartGreenhouse.Application.Services;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;
using SmartGreenhouse.Domain.Rules;
using SmartGreenhouse.Repository;
using SmartGreenhouse.Repository.Serial;


bool usarArduino = true; 
string puerto = "COM4";   


ISensorHumedad sensorHumedad;
IActuadorRiego bomba;

if (usarArduino)
{
    var adapter = new ArduinoSerialAdapter(puerto);
    sensorHumedad = new SensorHumedadSueloSerial(adapter, "HUM-01");
    bomba = new BombaAguaSerial(adapter);
    Console.WriteLine("[Sistema] Modo Arduino real");
}
else
{
    sensorHumedad = new SensorHumedadSuelo { Id = "HUM-01", PinArduino = 0 };
    bomba = new BombaAgua { PinRele = 7 };
    Console.WriteLine("[Sistema] Modo simulación");
}

var sensorTemp = new SensorTemperatura
{
    Id = "TEMP-01",
    PinArduino = 3,
    Unidad = "C"
};

var ventilador = new Ventilador
{
    PinPWM = 5
};

var repo = new FileGreenhouseRepository();

var regla = new ReglaRiego
{
    UmbralHumedadMinima = 50f,
    DuracionRiegoRecomendada = 30,
    HorasEntreRiegos = 6
};

var monitoreo = new SensorMonitoringService(sensorHumedad, sensorTemp);
var riego = new IrrigationService(repo, bomba, regla);
var ventilacion = new VentilationService(ventilador, umbralTemp: 30f);
var controller = new GreenhouseController(monitoreo, riego, ventilacion);


controller.IniciarSistema();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== SMART GREENHOUSE ===");
    Console.WriteLine("1. READ");
    Console.WriteLine("2. IRRIGATE ON");
    Console.WriteLine("3. IRRIGATE OFF");
    Console.WriteLine("4. FAN ON");
    Console.WriteLine("5. FAN OFF");
    Console.WriteLine("6. HISTORY");
    Console.WriteLine("7. SET MOISTURE_THRESHOLD");
    Console.WriteLine("8. SET TEMP_THRESHOLD");
    Console.WriteLine("9. EXIT");
    Console.Write("Seleccione una opcion: ");

    string? input = Console.ReadLine();

    if (input is null)
        continue;

    switch (input.Trim())
    {
        case "1":
            controller.EjecutarCicloMonitoreo();
            break;

        case "2":
            riego.ForzarRiegoManual(regla.DuracionRiegoRecomendada);
            break;

        case "3":
            riego.DetenerRiego();
            break;

        case "4":
            ventilacion.ForzarVentilacion(3);
            break;

        case "5":
            ventilacion.DetenerVentilacion();
            break;

        case "6":
            var historial = repo.ObtenerHistorial();
            if (historial.Count == 0)
            {
                Console.WriteLine("No hay eventos de riego registrados.");
            }
            else
            {
                Console.WriteLine($"{"Fecha",-22} {"Causa",-10} {"Duración",-10} {"Antes",-8} {"Después",-8} {"Exitoso",-8}");
                Console.WriteLine(new string('-', 70));
                foreach (var evento in historial)
                {
                    Console.WriteLine(
                        $"[{evento.Timestamp:yyyy-MM-dd HH:mm:ss}] " +
                        $"Causa={evento.Causa,-10} " +
                        $"Duracion={evento.DuracionSeg}s " +
                        $"Antes={evento.HumedadAntes:F2} " +
                        $"Despues={evento.HumedadDespues:F2} " +
                        $"Exitoso={evento.EsExitoso()}"
                    );
                }
            }
            break;

        case "7":
            Console.Write("Nuevo umbral de humedad (actual: " + regla.UmbralHumedadMinima + "): ");
            if (float.TryParse(Console.ReadLine(), out float nuevoUmbralHumedad))
            {
                riego.ActualizarUmbralRiego(nuevoUmbralHumedad);
                Console.WriteLine($"[Sistema] Umbral de humedad actualizado a {nuevoUmbralHumedad}.");
            }
            else
            {
                Console.WriteLine("[Error] Valor no válido.");
            }
            break;

        case "8":
            Console.Write("Nuevo umbral de temperatura (actual: " + ventilacion.UmbralTemp + "): ");
            if (float.TryParse(Console.ReadLine(), out float nuevoUmbralTemp))
            {
                ventilacion.ActualizarUmbralTemp(nuevoUmbralTemp);
                Console.WriteLine($"[Sistema] Umbral de temperatura actualizado a {nuevoUmbralTemp}.");
            }
            else
            {
                Console.WriteLine("[Error] Valor no válido.");
            }
            break;

        case "9":
            controller.DetenerSistema();
            Console.WriteLine("[Sistema] Hasta luego.");
            return;

        default:
            Console.WriteLine("[Error] Opción no válida. Seleccione entre 1 y 9.");
            break;
    }
}