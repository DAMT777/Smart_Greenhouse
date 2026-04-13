namespace SmartGreenhouse.Domain.Rules
{
    public class ReglaRiego
    {
        public float UmbralHumedadMinima { get; private set; }
        public int DuracionRiegoRecomendada { get; private set; }
        public int HorasEntreRiegos { get; private set; }

        public ReglaRiego(float umbralHumedadMinima, int duracionRiegoRecomendada, int horasEntreRiegos)
        {
            this.UmbralHumedadMinima = umbralHumedadMinima;
            this.DuracionRiegoRecomendada = duracionRiegoRecomendada;
            this.HorasEntreRiegos = horasEntreRiegos;
        }

        public bool requiereRiego(float humedadActual)
        {
            return humedadActual < UmbralHumedadMinima;
        }

        public int getDuracionRiego()
        {
            return DuracionRiegoRecomendada;
        }

        public void actualizarUmbral(float nuevoUmbral)
        {
            UmbralHumedadMinima = nuevoUmbral;
        }
    }
}
