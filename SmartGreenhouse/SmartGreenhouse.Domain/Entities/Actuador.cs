namespace SmartGreenhouse.Domain.Entities
{
    public abstract class Actuador
    {
        public string Id { get; set; }
        public bool Encendido { get; protected set; }
        public string Modo { get; set; }

        public virtual void encender()
        {
            Encendido = true;
        }

        public virtual void apagar()
        {
            Encendido = false;
        }

        public void setModo(string modo)
        {
            this.Modo = modo;
        }
    }
}
