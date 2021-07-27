using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSim
{
    public class Dispatcher 
    {
        public Dispatcher()
        {

        }
        public static bool InitializeDispatcher() //initializes the dispatcher
        {
            return true;
        }

        public static bool StopDispatcher()        //Stops dispatcher
        {
            return false;
        }

        public static ElevatorCar GetNearestAvailableElevator(int callingFloor, bool upOrDownButton)        //finds closest available elevator to respond to floor call
        {
            var nearestElevatorCarID = ElevatorSystem.AllElevators.Count;
            var distanceChecker = ElevatorSystem.AllFloors.Count +1;
            foreach (var elevator in ElevatorSystem.AllElevators)
            {
                var canBeCalled = elevator.IsMovingUp == upOrDownButton && elevator.CurrentFloor >= callingFloor;
                if (canBeCalled || elevator.IsMoving == false)
                {
                    var distance = ElevatorCar.DistanceFromCarToFloor(elevator, callingFloor);
                    distanceChecker = ElevatorCar.DistanceChecker(distance, distanceChecker, elevator,
                        nearestElevatorCarID, out nearestElevatorCarID);
                }
            }

            return ElevatorSystem.AllElevators[nearestElevatorCarID];
        }
        
    }
}
