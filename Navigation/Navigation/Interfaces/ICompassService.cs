using Navigation.Events;

namespace Navigation.Interfaces
{
    public interface ICompassService
    {
        event CompassHandler OnCompassDataChanged;
        void Start();
        void Stop();
    }
}
