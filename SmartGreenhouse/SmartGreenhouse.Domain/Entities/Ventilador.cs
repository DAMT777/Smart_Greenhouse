namespace SmartGreenhouse.Domain.Entities
{
    public class Ventilador : Actuador
    {
        public int Velocidad { get; private set; }

        public Ventilador(string id)
        {
            this.Id = id;
            this.Modo = "auto";
        }

        public void setVelocidad(int nivel)
        {
            Velocidad = nivel;
        }
    }
}
