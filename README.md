# SmartGreenhouse

SmartGreenhouse is a cyber-physical console application built with C# and .NET 10 that monitors and automates a smart greenhouse environment. It reads soil humidity and temperature data from sensor abstractions, evaluates configurable irrigation and ventilation thresholds, and drives actuators accordingly — all through a clean layered architecture designed for future Arduino hardware integration. The codebase is fully compilable and architecturally complete, with hardware interaction points stubbed out and ready to be replaced with real serial communication without touching any business logic.

---

## Table of Contents

- [About](#about)
- [Solution Structure](#solution-structure)
- [Architecture](#architecture)
- [GRASP Principles Applied](#grasp-principles-applied)
- [Console Commands](#console-commands)
- [Hardware Stub Policy](#hardware-stub-policy)
- [How to Run](#how-to-run)
- [Future Work](#future-work)

---

## About

SmartGreenhouse automates two critical greenhouse control loops:

- **Irrigation** — soil humidity is read continuously and compared against a configurable minimum threshold. When humidity drops below the threshold, the water pump is activated for a configurable duration and the event is logged to the repository.
- **Ventilation** — air temperature is evaluated against a configurable upper threshold. When the threshold is exceeded, the fan is switched on at full speed; otherwise it is switched off.

Both thresholds can be updated at runtime via console commands, making the system configurable without restarting the application. All dependencies are wired manually in `Program.cs` using constructor injection, with no third-party DI framework required.

---

## Solution Structure

```
SmartGreenhouse/
├── SmartGreenhouse.Domain/
│   ├── Entities/           → Sensor, SensorHumedadSuelo, SensorTemperatura,
│   │                          Actuador, BombaAgua, Ventilador,
│   │                          ClimateState, IrrigationEvent, SistemaInvernadero
│   ├── Interfaces/         → ISensorHumedad, IActuadorRiego, IGreenhouseRepository
│   └── Rules/              → ReglaRiego
├── SmartGreenhouse.Application/
│   └── Services/           → SensorMonitoringService, IrrigationService,
│                              VentilationService, GreenhouseController
├── SmartGreenhouse.Repository/
│                           → InMemoryGreenhouseRepo, FileGreenhouseRepository,
│                              SimuladorSensor
└── SmartGreenhouse.Presentation/
                            → Program.cs
```

---

## Architecture

The solution follows a strict **layered architecture** where each layer depends only on the layer directly below it:

```
Presentation  →  Application  →  Domain
                 Repository   →  Domain
```

| Layer | Responsibility |
|---|---|
| **Domain** | Core entities, business rules, and interfaces. No external dependencies. |
| **Application** | Orchestration services that coordinate sensors, actuators, and repositories through domain abstractions. |
| **Repository** | Concrete implementations of `IGreenhouseRepository`: in-memory store, file-based persistence, and sensor simulator. |
| **Presentation** | Entry point. Wires all dependencies manually and drives the interactive command loop. |

Key design decisions:

- The **Domain layer has zero external dependencies** — it does not reference any other project in the solution.
- All inter-layer communication goes through **interfaces** defined in the Domain (`ISensorHumedad`, `IActuadorRiego`, `IGreenhouseRepository`), keeping concrete implementations swappable.
- **Constructor injection** is used throughout. `Program.cs` is the single composition root where all objects are instantiated and wired together.
- Hardware is fully **abstracted behind interfaces**, meaning the Application layer never calls any platform-specific API directly.

---

## GRASP Principles Applied

| Principle | Where Applied |
|---|---|
| **Controller** | `GreenhouseController` acts as the single entry point for all system operations and console commands, delegating to specialized services. |
| **Information Expert** | `ReglaRiego` owns and evaluates the irrigation threshold (`UmbralHumedadMinima`) because it holds all the information needed to make that decision. |
| **Protected Variations** | `ISensorHumedad`, `IActuadorRiego`, and `IGreenhouseRepository` shield the Application layer from changes in hardware or persistence implementations. |
| **Pure Fabrication** | `SensorMonitoringService`, `IrrigationService`, and `VentilationService` do not represent domain concepts — they are invented classes that group related behaviour cohesively. |
| **High Cohesion** | Each service has a single, focused responsibility: monitoring, irrigation, or ventilation. None of them overlap. |
| **Low Coupling** | Services depend on domain interfaces, not concrete classes. Swapping `InMemoryGreenhouseRepo` for `FileGreenhouseRepository`, or a stub sensor for a real Arduino sensor, requires no changes in the Application layer. |

The project also applies:

- **Repository Pattern** via `IGreenhouseRepository`, which decouples data persistence from business logic.
- **Strategy-like Pattern** via `ISensorHumedad` and `IActuadorRiego`, allowing any sensor or actuator implementation to be substituted at the composition root.

---

## Console Commands

Once the application is running, the following commands are available at the `>` prompt:

| Command | Description |
|---|---|
| `READ` | Prints current humidity (%), temperature (°), and operating mode. |
| `IRRIGATE ON` | Forces the water pump on for 30 seconds. |
| `IRRIGATE OFF` | Stops the water pump immediately. |
| `IRRIGATE AUTO` | Runs a full monitoring cycle: reads sensors, evaluates rules, activates actuators if needed. |
| `SET MOISTURE_THRESHOLD <value>` | Updates the minimum humidity threshold used by `ReglaRiego`. Takes effect on the next evaluation cycle. |
| `SET TEMP_THRESHOLD <value>` | Updates the temperature threshold used by `VentilationService`. Takes effect on the next evaluation cycle. |
| `EXIT` | Stops all actuators cleanly and exits the application. |

---

## Hardware Stub Policy

No real hardware is connected in the current implementation. The stubs behave as follows:

- `SensorHumedadSuelo` and `SensorTemperatura` return `0f` when `leerValor()` is called.
- `SimuladorSensor` returns a fixed `valorSimulado` value, useful for development and demonstration.
- `BombaAgua` and `Ventilador` update internal state only — no GPIO, no serial port, no physical signal is produced.
- There is no `SerialPort`, no Arduino SDK reference, and no platform-specific API anywhere in the codebase.

**To connect real hardware**, only the stub classes need to be replaced with implementations that communicate over serial or GPIO. The interfaces (`ISensorHumedad`, `IActuadorRiego`) and all Application services remain unchanged. No business logic needs to be touched.

---

## How to Run

**Prerequisites:** [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

1. Clone or download the repository.
2. Open `SmartGreenhouse.sln` in Visual Studio 2022 or later.
3. Right-click `SmartGreenhouse.Presentation` in Solution Explorer and select **Set as Startup Project**.
4. Press `Ctrl + F5` to run without the debugger.
5. Type any of the [console commands](#console-commands) at the `>` prompt.

Alternatively, from the terminal:

```
cd SmartGreenhouse.Presentation
dotnet run
```

---

## Future Work

- **Arduino Serial Integration** — implement `ISensorHumedad` and `IActuadorRiego` using `System.IO.Ports.SerialPort` to read real sensor data and drive actuators over a USB connection to an Arduino board.
- **File-Based Persistence** — `FileGreenhouseRepository` is already scaffolded in the Repository layer. Completing its implementation will allow irrigation events and humidity readings to be persisted across sessions without any changes to the Application layer.
- **Unit Testing** — the interface-driven design makes every service fully testable in isolation. A dedicated test project using xUnit and Moq can be added to cover `ReglaRiego` decision logic, `IrrigationService` evaluation cycles, and `GreenhouseController` command dispatch.
- **Web Dashboard** — a minimal ASP.NET Core web API or Blazor dashboard could expose the same `GreenhouseController` over HTTP, enabling remote monitoring and threshold configuration from a browser or mobile device.
