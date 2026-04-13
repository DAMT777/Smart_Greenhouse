using System;
using System.Collections.Generic;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Repository
{
    public class FileGreenhouseRepository : IGreenhouseRepository
    {
        public string rutaArchivo { get; set; }
        public string formato { get; set; }

        public FileGreenhouseRepository(string rutaArchivo, string formato)
        {
            this.rutaArchivo = rutaArchivo;
            this.formato = formato;
        }

        public void guardarLecturaHumedad(float valor)
        {
            throw new NotImplementedException();
        }

        public void registrarEvento(IrrigationEvent evento)
        {
            throw new NotImplementedException();
        }

        public List<IrrigationEvent> obtenerHistorial()
        {
            throw new NotImplementedException();
        }
    }
}
