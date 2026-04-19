using System;
using System.IO.Ports;

namespace SmartGreenhouse.Repository.Serial;

public class ArduinoSerialAdapter : IDisposable
{
    private readonly SerialPort _serialPort;
    public string Puerto { get; }

    public ArduinoSerialAdapter(string puerto)
    {
        Puerto = puerto;
        _serialPort = new SerialPort(Puerto, 9600)
        {
            NewLine = "\n",
            ReadTimeout = 2000,
            WriteTimeout = 2000
        };

        _serialPort.Open();
        Console.WriteLine($"[Serial] Conectado a {puerto} @9600");
    }

    public string EnviarComando(string comando)
    {
        _serialPort.WriteLine(comando);
        string respuesta = _serialPort.ReadLine().Trim();
        Console.WriteLine($"[Serial:{Puerto}] {comando} <= {respuesta}");
        return respuesta;
    }

    public string SolicitarLectura(string comando)
    {
        _serialPort.WriteLine(comando);
        string respuesta = _serialPort.ReadLine().Trim();
        Console.WriteLine($"[Serial:{Puerto}] {comando} <= {respuesta}");
        return respuesta;
    }

    public void Dispose()
    {
        if (_serialPort.IsOpen)
        {
            _serialPort.Close();
        }

        _serialPort.Dispose();
    }
}
