using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSim
{
    public interface IButtonPanel
    {
        //interface ButtonPanel()
        // bool sendInstructionToDispatcher(int currFloor, int destFloor, bool isMovingUp)
        Task SendInstructionToDispatcher(int currentFloor, int destinationFloor, bool isMovingUp);
        string ElevatorFloorDisplay { get; }
        int Floor { get; }
        bool IsDoorOpen { get; }
        int TotalFloors { get; }

        Task DoorCloseEventHandlerAsync();
        Task DoorOpenEventHandlerAsync();
        Task FloorChangeEventHandlerAsync(int newFloor);
        Task PushDownCallAsync();
        Task PushUpCallAsync();
    }
}
