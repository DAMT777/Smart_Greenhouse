using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Domain.Entities;

public class Ventilador : IActuadorVentilacion
{
    private bool _activo;

    public int Velocidad { get; set; }

    public int PinPWM { get; set; }

    public void SetVelocidad(int nivel)
    {
        Velocidad = nivel;
        _activo = true;
        Console.WriteLine($"Ventilador activado con nivel {nivel}.");
    }

    public void Detener()
    {
        Velocidad = 0;
        _activo = false;
        Console.WriteLine("Ventilador detenido.");
    }

    public bool EstaActivo()
    {
        return _activo;
    }
}
