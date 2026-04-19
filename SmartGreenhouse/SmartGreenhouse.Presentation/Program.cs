using SmartGreenhouse.Application.Services;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;
using SmartGreenhouse.Domain.Rules;
using SmartGreenhouse.Repository;
using SmartGreenhouse.Repository.Serial;

public class ConsoleApp
{
    private static bool usarArduino = false;
    private static string puerto = "COM4";

    private GreenhouseController _controlador;
    private IrrigationService _riego;
    private VentilationService _ventilacion;
    private IGreenhouseRepository _repo;
    private ReglaRiego _regla;

    public ConsoleApp(
        GreenhouseController controlador,
        IrrigationService riego,
        VentilationService ventilacion,
        IGreenhouseRepository repo,
        ReglaRiego regla)
    {
        _controlador = controlador;
        _riego = riego;
        _ventilacion = ventilacion;
        _repo = repo;
        _regla = regla;
    }

    public static void Main(string[] args)
    {
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

        var app = new ConsoleApp(controller, riego, ventilacion, repo, regla);
        app.MostrarMenuPrincipal();
    }

    public void MostrarMenuPrincipal()
    {
        _controlador.IniciarSistema();

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

            ProcesarComando(input.Trim());
        }
    }

    public void ProcesarComando(string cmd)
    {
        switch (cmd)
        {
            case "1":
                _controlador.EjecutarCicloMonitoreo();
                break;

            case "2":
                _riego.ForzarRiegoManual(_regla.DuracionRiegoRecomendada);
                break;

            case "3":
                _riego.DetenerRiego();
                break;

            case "4":
                _ventilacion.ForzarVentilacion(3);
                break;

            case "5":
                _ventilacion.DetenerVentilacion();
                break;

            case "6":
                MostrarHistorial();
                break;

            case "7":
                ActualizarUmbralHumedad();
                break;

            case "8":
                ActualizarUmbralTemperatura();
                break;

            case "9":
                _controlador.DetenerSistema();
                Console.WriteLine("[Sistema] Hasta luego.");
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("[Error] Opción no válida. Seleccione entre 1 y 9.");
                break;
        }
    }

    public void IniciarMonitoreoContinuo()
    {
        Console.WriteLine("[Sistema] Iniciando monitoreo continuo. Presione Ctrl+C para detener.");
        while (true)
        {
            _controlador.EjecutarCicloMonitoreo();
            Thread.Sleep(5000);
        }
    }

    private void MostrarHistorial()
    {
        var historial = _repo.ObtenerHistorial();
        if (historial.Count == 0)
        {
            Console.WriteLine("No hay eventos de riego registrados.");
            return;
        }

        Console.WriteLine($"\n{"Fecha",-22} {"Causa",-10} {"Duración",-10} {"Antes",-8} {"Después",-8} {"Exitoso",-8}");
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

    private void ActualizarUmbralHumedad()
    {
        Console.Write($"Nuevo umbral de humedad (actual: {_regla.UmbralHumedadMinima}): ");
        if (float.TryParse(Console.ReadLine(), out float nuevoUmbral))
        {
            _riego.ActualizarUmbralRiego(nuevoUmbral);
            Console.WriteLine($"[Sistema] Umbral de humedad actualizado a {nuevoUmbral}.");
        }
        else
        {
            Console.WriteLine("[Error] Valor no válido.");
        }
    }

    private void ActualizarUmbralTemperatura()
    {
        Console.Write($"Nuevo umbral de temperatura (actual: {_ventilacion.UmbralTemp}): ");
        if (float.TryParse(Console.ReadLine(), out float nuevoUmbral))
        {
            _ventilacion.ActualizarUmbralTemp(nuevoUmbral);
            Console.WriteLine($"[Sistema] Umbral de temperatura actualizado a {nuevoUmbral}.");
        }
        else
        {
            Console.WriteLine("[Error] Valor no válido.");
        }
    }
}