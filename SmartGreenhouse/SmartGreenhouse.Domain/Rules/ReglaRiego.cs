namespace SmartGreenhouse.Domain.Rules
{
    public class ReglaRiego
    {
        public float umbralHumedadMinima { get; private set; }
        public int duracionRiegoRecomendada { get; private set; }
        public int horasEntreRiegos { get; private set; }

        public ReglaRiego(float umbralHumedadMinima, int duracionRiegoRecomendada, int horasEntreRiegos)
        {
            this.umbralHumedadMinima = umbralHumedadMinima;
            this.duracionRiegoRecomendada = duracionRiegoRecomendada;
            this.horasEntreRiegos = horasEntreRiegos;
        }

        public bool requiereRiego(float humedadActual)
        {
            return humedadActual < umbralHumedadMinima;
        }

        public int getDuracionRiego()
        {
            return duracionRiegoRecomendada;
        }

        public void actualizarUmbral(float nuevoUmbral)
        {
            umbralHumedadMinima = nuevoUmbral;
        }
    }
}
