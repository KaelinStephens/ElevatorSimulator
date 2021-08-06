using System;
using ElevatorFinalProject.Domain.Interfaces;
using ElevatorFinalProject.Domain.Models;

namespace ElevatorFinalProject.Infastructure
{
    public class DisplayService : IDisplayService
    {
        private string name;
        private IDisposable cancellation;

        public DisplayService(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("The observer must be assigned a name.");

            this.name = name;
        }

        public virtual void Subscribe(IEventService provider)
        {
            cancellation = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            cancellation.Dispose();
        }

        public virtual void OnCompleted()
        {
        }

        // No implementation needed: Method is not called by any class.
        public virtual void OnError(Exception e)
        {
            // No implementation.
        }

        // Update information.
        public virtual void OnNext(Event info)
        {
            bool updated = false;

            Console.WriteLine(info.Message);
        }
    }
}