using System;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;
using SmartGreenhouse.Domain.Rules;

namespace SmartGreenhouse.Application.Services;

public class IrrigationService
{
    private IGreenhouseRepository _repositorio;
    private IActuadorRiego _actuador;
    private ReglaRiego _regla;

    public IrrigationService(IGreenhouseRepository repositorio, IActuadorRiego actuador, ReglaRiego regla)
    {
        _repositorio = repositorio;
        _actuador = actuador;
        _regla = regla;
    }

    public void EvaluarYEjecutar(ClimateState state)
    {
        _repositorio.GuardarLecturaHumedad(state.Humedad);

        if (_regla.RequiereRiego(state.Humedad))
        {
            int duracion = _regla.GetDuracionRiego();
            _actuador.ActivarPor(duracion);

            var evento = new IrrigationEvent
            {
                DuracionSeg = duracion,
                Causa = "AUTO",
                Timestamp = DateTime.Now,
                HumedadAntes = state.Humedad,
                HumedadDespues = state.Humedad + 10f
            };

            _repositorio.RegistrarEvento(evento);
        }
    }

    public void ForzarRiegoManual(int segundos)
    {
        _actuador.ActivarPor(segundos);

        var evento = new IrrigationEvent
        {
            DuracionSeg = segundos,
            Causa = "MANUAL",
            Timestamp = DateTime.Now,
            HumedadAntes = 0f,
            HumedadDespues = 0f
        };

        _repositorio.RegistrarEvento(evento);
    }

    public void DetenerRiego()
    {
        _actuador.Desactivar();
    }

    public void ActualizarUmbralRiego(float nuevoUmbral)
    {
        _regla.ActualizarUmbral(nuevoUmbral);
    }
}
