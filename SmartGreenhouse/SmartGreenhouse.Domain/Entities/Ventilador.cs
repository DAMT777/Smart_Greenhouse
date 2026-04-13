namespace SmartGreenhouse.Domain.Entities
{
    public class Ventilador : Actuador
    {
        public int velocidad { get; private set; }

        public Ventilador(string id)
        {
            this.id = id;
            this.modo = "auto";
        }

        public void setVelocidad(int nivel)
        {
            velocidad = nivel;
        }
    }
}
