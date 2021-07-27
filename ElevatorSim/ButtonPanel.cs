using System;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorSim
{
    public class ButtonPanel : IButtonPanel
    {
        private readonly IElevatorCar _elevatorCar;
        private int floor;
        public bool isDoorOpen;
        private string elevatorFloorDisplay;
     
        public string ElevatorFloorDisplay
        {
            get { return Volatile.Read(ref elevatorFloorDisplay); }
            private set { Volatile.Write(ref elevatorFloorDisplay, value); }
        }

        private static int _floorCounter = 1;
        public ButtonPanel(IElevatorCar elevatorCar)
        {
            this._elevatorCar = elevatorCar;
            Floor = _floorCounter;
            Interlocked.Increment(ref _floorCounter);
           //elevatorCar.RegisterButtonPanel(this);
            IsDoorOpen = false;
            ElevatorFloorDisplay = "";
        }
        public int TotalFloors => ElevatorSystem.AllFloors.Count -1;
        public async Task SendInstructionToDispatcher(int currentFloor, int destinationFloor, bool isMovingUp)
        {
            throw new NotImplementedException();
        }

        public int Floor
        {
            get { return Volatile.Read(ref floor); }
            private set { Volatile.Write(ref floor, value); }
        }
        public bool IsDoorOpen
        {
            get { return Volatile.Read(ref isDoorOpen); }
            private set { Volatile.Write(ref isDoorOpen, value); }
        }
        public async Task PushUpCallAsync()
        {
            await _elevatorCar.UpCallRequestAsync(Floor).ConfigureAwait(false);
        }

        public async Task PushDownCallAsync()
        {
            await _elevatorCar.DownCallRequestAsync(Floor).ConfigureAwait(false);
        }

        public Task FloorChangeEventHandlerAsync(int newFloor)
        {
            ElevatorFloorDisplay = newFloor.ToString();
            return Task.CompletedTask;
        }

        public Task DoorCloseEventHandlerAsync()
        {
            IsDoorOpen = false;
            return Task.CompletedTask;
        }

        public Task DoorOpenEventHandlerAsync()
        {
            IsDoorOpen = true;
            return Task.CompletedTask;
        }

    }
}