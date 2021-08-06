using System.Threading.Tasks;
using ElevatorFinalProject.Domain.Entities;
using ElevatorFinalProject.Domain.Models;
using ElevatorFinalProject.Domain.Services;
using ElevatorFinalProject.Infastructure;

namespace ElevatorFinalProject.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var eventService = new EventService();
            var displayService = new DisplayService("Console Display Service");
            displayService.Subscribe(eventService);
            var commandService = new CallCommandService();
            var elevator = new Elevator(commandService, eventService, 0, 8);
            var floorButtonPanel = new FloorButtonPanel(commandService, eventService, 0, 8);
            var elevatorButton = new ElevatorButtonPanel(commandService, elevator, eventService);
            var aI = new AI(floorButtonPanel, elevatorButton);
            elevator.CallCommands.Add(new CallCommand(DirectionEnum.UP, 0));
            elevator.CallCommands.Add(new CallCommand(DirectionEnum.DOWN, 5));
            var task1 = StartAI(aI);
            var task2 = StartElevator(elevator);
            await Task.WhenAll(task1, task2);
            System.Console.ReadLine();
        }

        
        public static Task StartAI(AI aI)
        {
            aI._timer.Start();
            return Task.CompletedTask;
        }

        public static Task StartElevator(Elevator e)
        {
            //e.EmptyCallCommandBag();
            e._timer.Start();
            //System.Console.WriteLine("I'm working");
            return Task.CompletedTask;
        }
    }
}
