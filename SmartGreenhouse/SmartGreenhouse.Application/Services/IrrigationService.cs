using System;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;
using SmartGreenhouse.Domain.Rules;

namespace SmartGreenhouse.Application.Services
{
    public class IrrigationService
    {
        private IGreenhouseRepository repositorio;
        private IActuadorRiego actuador;
        private ReglaRiego regla;

        public IrrigationService(IGreenhouseRepository repositorio, IActuadorRiego actuador, ReglaRiego regla)
        {
            this.repositorio = repositorio;
            this.actuador = actuador;
            this.regla = regla;
        }

        public void evaluarYEjecutar(ClimateState state)
        {
            repositorio.guardarLecturaHumedad(state.Humedad);

            if (regla.requiereRiego(state.Humedad))
            {
                int duracion = regla.getDuracionRiego();
                actuador.activarPor(duracion);

                var evento = new IrrigationEvent(
                    duracion,
                    "automatico",
                    DateTime.Now,
                    state.Humedad,
                    0f
                );
                repositorio.registrarEvento(evento);
            }
        }

        public void forzarRiegoManual(int segundos)
        {
            actuador.activarPor(segundos);

            var evento = new IrrigationEvent(
                segundos,
                "manual",
                DateTime.Now,
                0f,
                0f
            );
            repositorio.registrarEvento(evento);
        }

        public void detenerRiego()
        {
            actuador.desactivar();
        }
    }
}
