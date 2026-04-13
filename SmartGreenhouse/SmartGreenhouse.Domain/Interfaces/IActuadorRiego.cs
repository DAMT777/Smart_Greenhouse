using System;
namespace SmartGreenhouse.Domain.Interfaces
{
    public interface IActuadorRiego
    {
        void desactivar();
        bool estaActivo();
        void activarPor(int seg);
    }
}
