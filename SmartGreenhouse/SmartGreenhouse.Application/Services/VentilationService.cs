using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Application.Services;

public class VentilationService
{
    private IActuadorVentilacion _ventilador;
    private float _umbralTemp;

    public VentilationService(IActuadorVentilacion ventilador, float umbralTemp)
    {
        _ventilador = ventilador;
        _umbralTemp = umbralTemp;
    }

    public void EvaluarTemperatura(ClimateState state)
    {
        if (state.Temperatura > _umbralTemp)
        {
            _ventilador.SetVelocidad(3);
        }
        else
        {
            _ventilador.Detener();
        }
    }

    public void ForzarVentilacion(int nivel)
    {
        _ventilador.SetVelocidad(nivel);
    }

    public void DetenerVentilacion()
    {
        _ventilador.Detener();
    }

    public void ActualizarUmbralTemp(float valor)
    {
        _umbralTemp = valor;
    }
}
