using System;
using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Repository.Serial;

public class BombaAguaSerial : IActuadorRiego
{
    private readonly ArduinoSerialAdapter _adapter;
    private bool _activa;

    public BombaAguaSerial(ArduinoSerialAdapter adapter)
    {
        _adapter = adapter;
    }

    public void ActivarPor(int seg)
    {
        _activa = true;
        string respuesta = _adapter.EnviarComando("BOMBA_ON");
        Console.WriteLine($"Bomba serial activada por {seg} segundos. Respuesta: {respuesta}");

        if (seg > 0)
        {
            Thread.Sleep(seg * 1000);
            Desactivar();
        }
    }

    public void Desactivar()
    {
        _activa = false;
        string respuesta = _adapter.EnviarComando("BOMBA_OFF");
        Console.WriteLine($"Bomba serial desactivada. Respuesta: {respuesta}");
    }

    public bool EstaActivo()
    {
        return _activa;
    }
}
