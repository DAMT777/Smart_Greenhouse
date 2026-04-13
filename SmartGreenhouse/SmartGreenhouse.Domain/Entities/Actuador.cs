namespace SmartGreenhouse.Domain.Entities
{
    public abstract class Actuador
    {
        public string id { get; set; }
        public bool encendido { get; protected set; }
        public string modo { get; set; }

        public virtual void encender()
        {
            encendido = true;
        }

        public virtual void apagar()
        {
            encendido = false;
        }

        public void setModo(string modo)
        {
            this.modo = modo;
        }
    }
}
