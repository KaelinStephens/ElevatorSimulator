using System.Collections.Concurrent;
using ElevatorFinalProject.Domain.Interfaces;
using ElevatorFinalProject.Domain.Models;

namespace ElevatorFinalProject.Domain.Services
{
    public class CallCommandService : ICallCommandService
    {
        public static ConcurrentBag<CallCommand> Commands { get; set; } = new ConcurrentBag<CallCommand>();
       
    }
}