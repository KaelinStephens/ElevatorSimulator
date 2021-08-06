using System.Threading.Tasks;
using ElevatorFinalProject.Domain.Interfaces;
using ElevatorFinalProject.Domain.Models;
using ElevatorFinalProject.Domain.Services;

namespace ElevatorFinalProject.Domain.Entities
{
    public class FloorButtonPanel : IFloorButtonPanel
    {
        private readonly ICallCommandService _callCommandService;
        private readonly IEventService _eventService;
        public int FloorId { get; set; }
        //private static int _count = 0;

        public string Display { get; set; }

        public FloorButtonPanel(ICallCommandService callCommandService, IEventService eventService, int startFloor, int floors = 5, int timerInterval = 2000, int stepDelay = 0)
        {
            _callCommandService = callCommandService;
            _eventService = eventService;
            //FloorId = Interlocked.Increment(ref _count) - 1;
            Display = GetDisplay(FloorId);
            FloorId = startFloor;
        }

        public string GetDisplay(int floorId)
        {
            switch (floorId)
            {
                case 0:
                    return "Lobby";
                default:
                    return $"Floor {floorId}";
            }
        }


        public Task UpButtonAsync()
        { 
            CallCommandService.Commands.Add(new CallCommand(DirectionEnum.UP, FloorId));
            _eventService.AddEvent($"Someone pushed the up button on {Display}", EventType.UP_BUTTON_CALL);
            return Task.CompletedTask;
        }
    
        public Task DownButtonAsync()
        {
            CallCommandService.Commands.Add(new CallCommand(DirectionEnum.DOWN, FloorId));
            _eventService.AddEvent($"Someone pushed the down button on {Display}", EventType.DOWN_BUTTON_CALL);
            return Task.CompletedTask;
        }
    }

   
}
