using System;
using System.Collections.Generic;
using SmartGreenhouse.Domain.Entities;
using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Repository
{
    public class FileGreenhouseRepository : IGreenhouseRepository
    {
        public string RutaArchivo { get; set; }
        public string Formato { get; set; }

        public FileGreenhouseRepository(string rutaArchivo, string formato)
        {
            this.RutaArchivo = rutaArchivo;
            this.Formato = formato;
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
