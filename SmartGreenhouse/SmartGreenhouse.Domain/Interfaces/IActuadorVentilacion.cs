namespace SmartGreenhouse.Domain.Interfaces;

public interface IActuadorVentilacion
{
    void SetVelocidad(int nivel);

    void Detener();

    bool EstaActivo();
}
