# SmartGreenhouse — .NET 8 Implementation

A Smart Greenhouse management system built in C# following Clean Architecture principles, implemented from a UML class diagram. The codebase is fully compilable and architecturally complete, with hardware interaction points stubbed out and ready for future integration.

---

## Solution Structure

```
SmartGreenhouse/
├── SmartGreenhouse.Domain/          # Core business logic, no external dependencies
│   ├── Entities/                    # Domain objects
│   ├── Interfaces/                  # Abstractions for sensors, actuators, repository
│   └── Rules/                       # Business rules
├── SmartGreenhouse.Application/     # Use cases and orchestration
│   └── Services/                    # Application services
├── SmartGreenhouse.Repository/      # Data persistence implementations
└── SmartGreenhouse.Presentation/    # Console entry point
```

### Project References

```
Domain        (no dependencies)
   ↑
Application   → Domain
Repository    → Domain
Presentation  → Application + Repository
```

---

## What Was Implemented

### Domain — Entities

| File | Description |
|------|-------------|
| `ClimateState.cs` | Value object holding a sensor reading snapshot: humidity, temperature, timestamp, and current mode. Includes `esValido()` to validate ranges (0–100% humidity, -50–50°C). |
| `Sensor.cs` | Abstract base class for all sensors. Holds `id`, `ultimaLectura`, and `tipo`. Declares abstract `leerValor()`. |
| `SensorHumedadSuelo.cs` | Extends `Sensor`, implements `ISensorHumedad`. Has a `calibracion` field. `leerValor()` returns `0f` — stub ready for hardware. |
| `SensorTemperatura.cs` | Extends `Sensor`. Has a `unidad` field (e.g., Celsius). `leerValor()` returns `0f` — stub ready for hardware. |
| `Actuador.cs` | Abstract base class for all actuators. Holds `id`, `encendido`, and `modo`. Provides `encender()`, `apagar()`, `setModo()`. |
| `BombaAgua.cs` | Extends `Actuador`, implements `IActuadorRiego`. Tracks `tiempoRiego`. `activarPor()`, `desactivar()`, and `estaActivo()` are stubs — no relay or serial logic. |
| `Ventilador.cs` | Extends `Actuador`. Adds `velocidad` and `setVelocidad(nivel)` — stub, no hardware logic. |
| `IrrigationEvent.cs` | Value object recording a single irrigation event: duration, cause, timestamp, humidity before and after. `esExitoso()` returns true when `humedadDespues > humedadAntes`. |
| `SistemaInvernadero.cs` | Represents the greenhouse configuration: humidity/temperature thresholds, operating mode, and name. Exposes `getNombre()`. |

### Domain — Rules

| File | Description |
|------|-------------|
| `ReglaRiego.cs` | Encapsulates the irrigation business rule. `requiereRiego(humedad)` returns true when the reading is below the minimum threshold. `getDuracionRiego()` returns the recommended irrigation duration. `actualizarUmbral()` allows runtime threshold changes. |

### Domain — Interfaces

These were pre-provided and completed with the missing `using` directives:

- `ISensorHumedad` — contract for any humidity sensor (`leerValor()`)
- `IActuadorRiego` — contract for any irrigation actuator (`activarPor`, `desactivar`, `estaActivo`)
- `IGreenhouseRepository` — contract for persistence (`guardarLecturaHumedad`, `registrarEvento`, `obtenerHistorial`)

### Repository

| File | Description |
|------|-------------|
| `InMemoryGreenhouseRepo.cs` | Full in-memory implementation of `IGreenhouseRepository`. Uses two `List<T>` fields to store humidity readings and irrigation events during the session. |
| `FileGreenhouseRepository.cs` | Stub implementation of `IGreenhouseRepository`. All three methods throw `NotImplementedException` — the structure is in place for a future file-based persistence layer. |
| `SimuladorSensor.cs` | Implements `ISensorHumedad`. Returns a fixed `valorSimulado` value with no randomness or variation — used in place of a real sensor for testing and demo purposes. |

### Application — Services

| File | Description |
|------|-------------|
| `SensorMonitoringService.cs` | Reads from both sensors and assembles a `ClimateState` snapshot. `validarLectura(valor)` checks that a reading is within the valid 0–100 range. |
| `IrrigationService.cs` | Coordinates the irrigation decision loop. `evaluarYEjecutar(state)` uses `ReglaRiego` to decide whether to irrigate, activates the actuator if needed, and persists the event. Also supports `forzarRiegoManual(segundos)` and `detenerRiego()`. |
| `VentilationService.cs` | Compares the current temperature in a `ClimateState` against a configured threshold. Turns the fan on at full speed when exceeded, turns it off otherwise. Supports `forzarVentilacion(nivel)` and `detenerVentilacion()`. |
| `GreenhouseController.cs` | Top-level orchestrator. `ejecutarCicloMonitoreo()` reads sensors and passes the state to both the irrigation and ventilation services. `procesarComando(cmd)` parses text commands and delegates to the correct service. |

### Presentation

`Program.cs` manually wires all dependencies (no DI framework) and runs a console menu loop:

```
> READ                          — prints current humidity, temperature, and mode
> IRRIGATE ON                   — forces irrigation for 30 seconds
> IRRIGATE OFF                  — stops irrigation immediately
> IRRIGATE AUTO                 — runs a full monitoring cycle
> SET MOISTURE_THRESHOLD <val>  — updates the humidity threshold in ReglaRiego
> SET TEMP_THRESHOLD <val>      — updates the temperature threshold in VentilationService
> EXIT                          — stops all actuators and exits
```

---

## Hardware Stub Policy

No real hardware communication was implemented. Every class that touches physical devices follows this contract:

- `SensorHumedadSuelo.leerValor()` → returns `0f`
- `SensorTemperatura.leerValor()` → returns `0f`
- `SimuladorSensor.leerValor()` → returns the fixed `valorSimulado`
- `BombaAgua` methods → update internal state only, no serial/relay logic
- `Ventilador.setVelocidad()` → updates internal state only

There is no `SerialPort`, no Arduino communication, and no GPIO anywhere in the codebase. To connect real hardware, replace only the stub implementations — the interfaces and service layer require no changes.

---

## Build

```bash
dotnet build
```

Expected output: **0 errors**. A few nullable reference warnings appear on abstract base class properties (`id`, `tipo`, `modo`) because they are assigned in subclass constructors rather than the abstract class itself — these are harmless and do not affect runtime behavior.
