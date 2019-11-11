public interface IObservable
{
    void AddObserver();
    void RemoveObserver(IObserver observer);
    void Notify();
}