using System;
using ElevatorFinalProject.Domain.Models;

namespace ElevatorFinalProject.Infastructure
{
    public interface IDisplayService : IObserver<Event>
    {
    }
}
