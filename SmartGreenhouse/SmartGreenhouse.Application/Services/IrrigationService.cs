using System;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;
using SmartGreenhouse.Domain.Rules;

namespace SmartGreenhouse.Application.Services
{
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

        public void evaluarYEjecutar(ClimateState state)
        {
            _repositorio.guardarLecturaHumedad(state.Humedad);

            if (_regla.requiereRiego(state.Humedad))
            {
                int duracion = _regla.getDuracionRiego();
                _actuador.activarPor(duracion);

                var evento = new IrrigationEvent(
                    duracion,
                    "automatico",
                    DateTime.Now,
                    state.Humedad,
                    0f
                );
                _repositorio.registrarEvento(evento);
            }
        }

        public void forzarRiegoManual(int segundos)
        {
            _actuador.activarPor(segundos);

            var evento = new IrrigationEvent(
                segundos,
                "manual",
                DateTime.Now,
                0f,
                0f
            );
            _repositorio.registrarEvento(evento);
        }

        public void detenerRiego()
        {
            _actuador.desactivar();
        }
    }
}
