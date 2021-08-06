using System;
using System.Collections.Generic;
using ElevatorFinalProject.Domain.Interfaces;
using ElevatorFinalProject.Domain.Models;

namespace ElevatorFinalProject.Domain.Services
{
    public class EventService : IEventService
    {
            private readonly List<IObserver<Event>> _observers;

            public EventService()
            {
                _observers = new List<IObserver<Event>>();
            }

            public IDisposable Subscribe(IObserver<Event> observer)
            {
                // Check whether observer is already registered. If not, add it
                if (!_observers.Contains(observer))
                {
                    _observers.Add(observer);
                }
                return new Unsubscriber<Event>(_observers, observer);
            }

            public void AddEvent(string message, EventType type)
            {
                var info = new Event(message, type);

                foreach (var observer in _observers)
                    observer.OnNext(info);
                
            }
    }
}