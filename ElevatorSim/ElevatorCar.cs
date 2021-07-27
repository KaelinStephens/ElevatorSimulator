using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ElevatorSim
{
    public class ElevatorCar : IElevatorCar
    {
        public HashSet<int> UpCalls => Volatile.Read(ref upCalls);

        public HashSet<int> DownCalls => Volatile.Read(ref downCalls);

        private ConcurrentDictionary<int, IButtonPanel> ExteriorCallPanels { get; }

        private DirectionEnum currentDirection = DirectionEnum.Stationary; // Doesn't need to be syncronized. Only used by the single update thread

        private readonly IElevatorCar elevatorCar;
        private readonly IElevator elevator;
        public int CurrentFloor                                              //floor where car currently is. Resets each time elevator moves to new floor.
        {
            get { return Volatile.Read(ref currentFloor); }
            private set { Volatile.Write(ref currentFloor, value); }
        }

        private readonly Timer timer = new Timer();
        private readonly ElevatorServiceUtilities elevatorServiceUtilities;
        private bool requestStop;
        public int TotalFloors => ElevatorSystem.AllFloors.Count;
        public int TotalElevators => ElevatorSystem.AllElevators.Count;

        private HashSet<int> upCalls = new HashSet<int>();
        private HashSet<int> downCalls = new HashSet<int>();

        private int currentFloor;
        
        public int ElevatorID { get; set; }     //unique id for each elevator car
        public bool IsMoving { get; set; }      //true if car is moving. False if car is stationary.
        public bool IsMovingUp { get; set; }        //only true if IsMoving == true AND car is moving to higher floor number
                                                    //false if IsMoving == false
                                                    //false if IsMoving == true AND car is moving to lower floor number

        public ElevatorCar(IElevatorCar elevatorCar)        //ElevatorCar()
        {
            this.elevatorCar = elevatorCar;                     
            ElevatorSystem.AllElevators.Add(this.elevatorCar);          //adds new-formed elevatorCar to ElevatorSystem list AllElevators
            SetupTimer();
            elevatorServiceUtilities = new ElevatorServiceUtilities(this);
            ExteriorCallPanels = new ConcurrentDictionary<int, IButtonPanel>();
        }

        private void SetupTimer()
        {
            timer.Interval = 100;
            // timer.Elapsed += PerformElevatorActionIteration;
            timer.AutoReset = false;
        }

  


        

        public  async Task MoveToFloor(int floorToMoveTo)
        {

        }

        
        //gets distance between individual elevator car and floor where button was pushed
        public static int DistanceFromCarToFloor(ElevatorCar elevator, int callingFloor)  
        {                                                       
            var diffIsPositive = callingFloor - elevator.CurrentFloor;
            return (diffIsPositive < 0 ? diffIsPositive * -1 : diffIsPositive);
        }

        //if distance of current elevator is closer than previous elevator, distanceChecker and nearestElevatorCarID get updated and returned.
        public static int DistanceChecker(int distance, int distanceChecker, ElevatorCar elevator, int currentNearestElevatorCarID, out int nearestElevatorCarID)
        {
            if (distance < distanceChecker)
            {
                distanceChecker = distance;
                nearestElevatorCarID = elevator.ElevatorID;
            }
            else
            {
                nearestElevatorCarID = currentNearestElevatorCarID;
            }
            return distanceChecker;
        }

        // moveToFloor(int floorToMoveTo)
        private async Task PerformNecessaryMovement()
        {
            // TODO: Get proper logging in instead of console write lines
            switch (currentDirection)
            {
                case DirectionEnum.Stationary:
                    return;
                case DirectionEnum.Up:
                    Console.WriteLine($"Moving up from floor {CurrentFloor}");
                    await elevator.MoveUpAsync().ConfigureAwait(false);
                    Interlocked.Increment(ref currentFloor);
                    Console.WriteLine($"Arrived at {CurrentFloor}");
                    //                    await UpdateFloorDisplays().ConfigureAwait(false); // TODO: use .net messaging
                    break;
                default:
                    Console.WriteLine($"Moving from floor {CurrentFloor} to {CurrentFloor - 1}");
                    await elevator.MoveDownAsync().ConfigureAwait(false);
                    Interlocked.Decrement(ref currentFloor);
                    //                    await UpdateFloorDisplays().ConfigureAwait(false);
                    break;
            }
        }

        public Task UpCallRequestAsync(int floor)
        {
            lock (UpCalls) UpCalls.Add(floor);
            return Task.CompletedTask;

        }

        public Task DownCallRequestAsync(int floor)
        {
            lock (DownCalls) DownCalls.Add(floor);
            return Task.CompletedTask;
        }

        public IButtonPanel GetCallPanelForFloor(int floor)
        {
            return ExteriorCallPanels[floor];
        }

        public Task StopAsync()
        {
            Volatile.Write(ref requestStop, true);
            timer.Stop();
            return Task.CompletedTask;
        }

        public Task StartAsync()
        {
            Volatile.Write(ref requestStop, false);
            timer.Start();
            return Task.CompletedTask;
        }

        public void RegisterCallPanel(IButtonPanel newPanel)
        {
            ExteriorCallPanels.TryAdd(newPanel.Floor, newPanel);
        }
    }
    
    public enum DirectionEnum
    {
        Up, Down, Stationary
    }
}