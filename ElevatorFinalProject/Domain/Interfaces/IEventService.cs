using System;
using ElevatorFinalProject.Domain.Models;

namespace ElevatorFinalProject.Domain.Interfaces
{
    public interface IEventService : IObservable<Event>
    {
        void AddEvent(string message, EventType type);
    }
}
