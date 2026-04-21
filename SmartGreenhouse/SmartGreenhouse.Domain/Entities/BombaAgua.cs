using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Domain.Entities;

public class BombaAgua : IActuadorRiego
{
    private bool _activo;

    public int TiempoRiego { get; set; }

    public int PinRele { get; set; }

    public void ActivarPor(int seg)
    {
        _activo = true;
        TiempoRiego = seg;
        Console.WriteLine($"Bomba de agua activada por {seg} segundos.");
    }

    public void Desactivar()
    {
        _activo = false;
        Console.WriteLine("Bomba de agua desactivada.");
    }

    public bool EstaActivo()
    {
        return _activo;
    }
}
