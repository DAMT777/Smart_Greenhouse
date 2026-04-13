using SmartGreenhouse.Domain.Interfaces;

namespace SmartGreenhouse.Domain.Entities
{
    public class BombaAgua : Actuador, IActuadorRiego
    {
        public int tiempoRiego { get; set; }

        public BombaAgua(string id)
        {
            this.id = id;
            this.modo = "manual";
        }

        public void activarPor(int seg)
        {
            tiempoRiego = seg;
            encendido = true;
        }

        public void desactivar()
        {
            encendido = false;
            tiempoRiego = 0;
        }

        public bool estaActivo()
        {
            return encendido;
        }
    }
}
