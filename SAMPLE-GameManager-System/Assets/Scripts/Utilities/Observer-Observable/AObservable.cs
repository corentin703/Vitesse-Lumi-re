using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AObservable : MonoBehaviour
{
    protected List<IObserver> _observers;

    public void RegisterObservator(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterObservator(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (IObserver observer in _observers)
        {
            observer.OUpdate();
        }
    }

    public void NotifyObserver(IObserver observer)
    {
        observer.OUpdate();
    }
}
