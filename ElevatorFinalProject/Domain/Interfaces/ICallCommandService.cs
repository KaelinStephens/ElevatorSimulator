using System.Collections.Concurrent;
using ElevatorFinalProject.Domain.Models;

namespace ElevatorFinalProject.Domain.Interfaces
{
    public interface ICallCommandService
    {
        public static ConcurrentBag<CallCommand> Commands { get; }
    }
}
