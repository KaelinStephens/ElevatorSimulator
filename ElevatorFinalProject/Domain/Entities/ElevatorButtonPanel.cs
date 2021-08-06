using System.Collections.Generic;
using System.Threading.Tasks;
using ElevatorFinalProject.Domain.Interfaces;
using ElevatorFinalProject.Domain.Models;
using ElevatorFinalProject.Domain.Services;

namespace ElevatorFinalProject.Domain.Entities
{
    public class ElevatorButtonPanel : IElevatorButtonPanel
    {
        private readonly IEventService _eventService;
        private readonly ICallCommandService _callCommandService;
        private readonly Elevator _elevator;
        public Dictionary<string, int> ButtonDictionary { get; }

        public ElevatorButtonPanel(ICallCommandService callCommandService, Elevator elevator, IEventService eventService)
        {
            _eventService = eventService;
            _callCommandService = callCommandService;
            _elevator = elevator;
            ButtonDictionary = new Dictionary<string, int>();
            GetButtonDictionary(_elevator._floors);
        }

        private void GetButtonDictionary(int elevatorFloors)
        {
            for (int i = 0; i <= elevatorFloors; i++)
            {
                switch (i)
                {
                    case 0:
                        ButtonDictionary.Add("Lobby", i);
                        break;
                    default:
                        ButtonDictionary.Add($"{i}", i);
                        break;
                }
            }
        }

        public Task PushButtonAsync(string buttonName)
        {
            CallCommandService.Commands.Add(new CallCommand(DiscoverDirection(buttonName), ButtonDictionary[buttonName]));
            _eventService.AddEvent($"The {buttonName} button lit up", EventType.ELEVATOR_BUTTON_PANEL_LIT);
            return Task.CompletedTask;
        }

        private DirectionEnum DiscoverDirection(string buttonName)
        {
            return ButtonDictionary[buttonName] > _elevator.CurrentFloor ? DirectionEnum.UP : DirectionEnum.DOWN;
        }
    }
}