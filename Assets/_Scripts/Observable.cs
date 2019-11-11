using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Observerable : MonoBehaviour, IObservable
{
    private List<IObserver> _observersList = new List<IObserver>();

    public void AddObserver()
    {
        var observers = FindObjectsOfType<MonoBehaviour>().OfType<IObserver>();

        foreach (var observer in observers)
        {
            _observersList.Add(observer);
        }
    }

    public void RemoveObserver(IObserver observer)
    {
        _observersList.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observersList)
        {
            observer.DataUpdate();
        }
    }
}
