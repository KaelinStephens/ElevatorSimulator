using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using ElevatorFinalProject.Domain.Interfaces;
using ElevatorFinalProject.Domain.Models;
using ElevatorFinalProject.Domain.Services;
using static ElevatorFinalProject.Domain.Models.EventType;
using Timer = System.Timers.Timer;

namespace ElevatorFinalProject.Domain.Entities
{
    public class Elevator
    {
        // private static int threadCount = 0;
        // private int id = 0;
        private readonly ICallCommandService _callCommandService;
        private readonly IEventService _eventService;
        private readonly Elevator _elevator;
        public HashSet<CallCommand> CallCommands { get; set; } = new HashSet<CallCommand>();
        internal readonly int _floors;
        private readonly int _stepDelay;
        private static int _count = 0;
        public int Id { get; set; }
        public Timer _timer = new Timer();
        public int TopFloor { get; set; }
        internal int CurrentFloor { get; set; }
        public DirectionEnum Direction { get; set; }

        public Elevator(ICallCommandService callCommandService, IEventService eventService, int startFloor,
            int floors = 5, int timerInterval = 2000, int stepDelay = 500)
        {
            _callCommandService = callCommandService;
            _eventService = eventService;
            _floors = floors;
            _stepDelay = stepDelay;
            Id = Interlocked.Increment(ref _count) - 1;
            Direction = DirectionEnum.STATIONARY;
            SetupTimer();
            CurrentFloor = startFloor;
            _elevator = this;
            TopFloor = floors;
            StartAsync();
        }

        public async Task
            MoveUpAsync() // method: 1. calls the Move up floor event 2. pauses for realistic effect 3. adds 1 to CurrentFloor (resetting it) 4. calls the arrival event
        {
            _eventService.AddEvent("Going up", MOVE_UP_FLOOR);
            await Task.Delay(_stepDelay);
            CurrentFloor++;
            _eventService.AddEvent($"Arrived at floor {CurrentFloor}", ARRIVAL);
        }

        private void SetupTimer() // sets up timer
        {
            _timer.Interval = 2000;
            _timer.Elapsed += ProcessCallCommandsAsync;
            _timer.AutoReset = false;
        }

        public async void
            ProcessCallCommandsAsync(object sender,
                ElapsedEventArgs e) // Note: shouldn't need a lock due to this restarting its own timer
        {
            Interlocked.Increment(ref _count);
            // threadCount = _count;
            while (CallCommands.Count == 0 || CallCommands == null)
            {
                EmptyCallCommandBagIntoHashSet();
            } // Method  

            while (CallCommands.Count != 0) // 1. if Direction is not Stationary 
            {
                // Console.WriteLine("Thread ID = "+ threadCount);
                //      a. checks for new call commands in the HashSet at each floor
                EmptyCallCommandBagIntoHashSet(); //      b. opens the elevator door (if necessary)  
                if (Direction != DirectionEnum.STATIONARY) //      c. moves elevator to the next floor  
                {
                    //      d. if the new floor moved to is the top or bottom floor
                    await PerformNecessaryDoorOperationsAndUpdateCommands(); //              aa. checks if door should open and does so if needed
                    await MoveUpOrDown1FloorAsync(); //              bb. double checks if elevator's at the top or bottom floor and changes Direction to Stationary if it is
                    if (_elevator.IsAtApex(CurrentFloor, _floors, CallCommands) ||
                        _elevator.IsAtNadir(CurrentFloor, _floors, CallCommands))
                    {
                        await PerformNecessaryDoorOperationsAndUpdateCommands(); // 2. if Direction is Stationary (either after completing step 1 or after skipping step 1)
                    }
                    ClearDirectionIfNecessary(); //      a. chooses the optimal CallCommand to fulfill next and sets Direction to match
                    // 3. Empties all new CallCommands from the Bag into the HashSet
                } 

                if (Direction == DirectionEnum.STATIONARY)
                {
                    DetermineNewDirection();
                }
            }

            _timer.Start(); // 4. restarts the timer
        }

        /// <summary>
        ///  checks if elevator's at the top or bottom floor 2. if it is, Direction is set to Stationary
        /// </summary>
        private void ClearDirectionIfNecessary() 
        {
            if (Direction == DirectionEnum.UP && _elevator.IsAtApex(CurrentFloor, TopFloor, CallCommands))
            {
                Direction = DirectionEnum.STATIONARY;
            }
            else if (Direction == DirectionEnum.DOWN && _elevator.IsAtNadir(CurrentFloor, TopFloor, CallCommands))
            {
                Direction = DirectionEnum.STATIONARY;
            }
        }

        public async Task
            MoveDownAsync() // method: 1. calls the Move down floor event 2. pauses for realistic effect 3. subtracts 1 from CurrentFloor (resetting it) 4. calls the arrival event
        {
            _eventService.AddEvent("Going down", MOVE_DOWN_FLOOR);
            await Task.Delay(_stepDelay);
            CurrentFloor--;
            _eventService.AddEvent($"Arrived at floor {CurrentFloor}", ARRIVAL);
        }

        public void
            EmptyCallCommandBagIntoHashSet() // takes any new call commands out of the bag and adds them to the HashSet if the HashSet does not already contain that particular command
        {
            while (CallCommandService.Commands.Any())
            {
                CallCommandService.Commands.TryTake(out var command);
                if (!CallCommands.Contains(command))
                    CallCommands.Add(command);
            }
        }

        public async Task  OpenDoorAsync() // method: 1. calls the door open event 2. pauses for realistic effect 3. calls the door close event
        {
            _eventService.AddEvent($"Door is now open on {CurrentFloor}", DOOR_OPEN);
            await Task.Delay(_stepDelay);
            _eventService.AddEvent($"Door is now closed on {CurrentFloor}", DOOR_CLOSE);
        }

        public Task StartAsync() // starts the timer when elevator is ready for use
        {
            _timer.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            _timer.Stop();
            return Task.CompletedTask;
        }

        public async Task PerformNecessaryDoorOperationsAndUpdateCommands() //Method: 1. checks if the elevator door needs to open 2. calls OpenDoorAsync() method if needed 3. if step 2 occured, removes the command from the hashset which made door open
        {
            var elevatorState =
                new CallCommand(Direction,
                    CurrentFloor); //This new call will work if someone in the elevator wants to get off on the current level and /or if someone calls the elevator on the current level (going the same direction)
            if (CallCommands.Contains(elevatorState))
            {
                await OpenDoorAsync();
                CallCommands
                    .Remove(elevatorState); //removes elevatorState from HashSet because that call has been fulfilled 
                _eventService.AddEvent($"The {CurrentFloor} button is no longer lit",
                    EventType.ELEVATOR_BUTTON_PANEL_NOT_LIT);
            }
        }

        public async Task
            MoveUpOrDown1FloorAsync() // method is a switch/case for deciding if the elevator needs to move up or down and calls the appropriate method 
        {
            switch (Direction)
            {
                case DirectionEnum.UP when CurrentFloor < _floors:
                    await MoveUpAsync();
                    break;
                case DirectionEnum.DOWN when CurrentFloor > 0:
                    await MoveDownAsync();
                    break;
            }
        }

        public void
            DetermineNewDirection() // method: 1. When elevator Direction is Stationary, checks if the current floor has an up or down call 2. if current floor has an up or down call in HashSet, elevator Direction
        {
            // will be set to match 3. if the current floor has both or neither calls the extention method for choosing the best option is called.
            var checkDownCalls = new CallCommand(DirectionEnum.DOWN, CurrentFloor);
            var checkUpCalls = new CallCommand(DirectionEnum.UP, CurrentFloor);
            if (CallCommands.Contains(checkDownCalls) && !CallCommands.Contains(checkUpCalls))
            {
                Direction = DirectionEnum.DOWN;
            }
            else if (CallCommands.Contains(checkUpCalls) && !CallCommands.Contains(checkDownCalls))
            {
                Direction = DirectionEnum.UP;
            }
            else
            {
                Direction = _elevator.SelectMostAppropriateDirectionBasedOnHeuristic(CurrentFloor, TopFloor,
                    CallCommands);
            }

        }
    }
}