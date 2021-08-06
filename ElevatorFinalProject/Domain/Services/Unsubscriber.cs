using System;
using System.Collections.Generic;

namespace ElevatorFinalProject.Domain.Services
{
    public class Unsubscriber<Event> : IDisposable
    {
        private List<IObserver<Event>> _observers;
        private IObserver<Event> _observer;

        public  Unsubscriber(List<IObserver<Event>> observers, IObserver<Event> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}