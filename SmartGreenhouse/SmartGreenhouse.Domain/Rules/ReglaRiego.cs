namespace SmartGreenhouse.Domain.Rules;

public class ReglaRiego
{
    public float UmbralHumedadMinima { get; set; }

    public int DuracionRiegoRecomendada { get; set; }

    public int HorasEntreRiegos { get; set; }

    public bool RequiereRiego(float humedadActual)
    {
        return humedadActual < UmbralHumedadMinima;
    }

    public int GetDuracionRiego()
    {
        return DuracionRiegoRecomendada;
    }

    public void ActualizarUmbral(float nuevoUmbral)
    {
        UmbralHumedadMinima = nuevoUmbral;
    }
}
