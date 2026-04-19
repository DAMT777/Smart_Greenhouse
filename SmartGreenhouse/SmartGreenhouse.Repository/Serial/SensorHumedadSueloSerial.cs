using System.Globalization;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Repository.Serial;

public class SensorHumedadSueloSerial : Sensor, ISensorHumedad
{
    private readonly ArduinoSerialAdapter _adapter;

    public SensorHumedadSueloSerial(ArduinoSerialAdapter adapter, string id)
    {
        _adapter = adapter;
        Id = id;
    }

    public override float LeerValor()
    {
        string lectura = _adapter.SolicitarLectura("READ_HUMEDAD");

        if (float.TryParse(lectura, NumberStyles.Float, CultureInfo.InvariantCulture, out float valor))
        {
            UltimaLectura = valor;
        }
        else
        {
            UltimaLectura = 0f;
        }

        return UltimaLectura;
    }
}
