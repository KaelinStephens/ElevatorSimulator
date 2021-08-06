namespace ElevatorFinalProject.Domain.Models
{
    public class Event
    {
        public string Message { get; }
        public EventType EventType { get; }

        public Event(string message, EventType type)
        {
            Message = message;
            EventType = type;
        }
    }
}