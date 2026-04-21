namespace SmartGreenhouse.Domain.Interfaces;

public interface IActuadorRiego
{
    void ActivarPor(int seg);

    void Desactivar();

    bool EstaActivo();
}
