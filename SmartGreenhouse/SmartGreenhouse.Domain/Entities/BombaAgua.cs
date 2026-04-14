using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Domain.Entities
{
    public class BombaAgua : Actuador, IActuadorRiego
    {
        public int TiempoRiego { get; set; }

        public BombaAgua(string id)
        {
            this.Id = id;
            this.Modo = "manual";
        }

        public void activarPor(int seg)
        {
            TiempoRiego = seg;
            Encendido = true;
        }

        public void desactivar()
        {
            Encendido = false;
            TiempoRiego = 0;
        }

        public bool estaActivo()
        {
            return Encendido;
        }
    }
}
