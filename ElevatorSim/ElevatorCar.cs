namespace ElevatorSim
{
    public class ElevatorCar
    {
        public ElevatorCar()        //ElevatorCar()
        {

        }
        public int ElevatorID { get; set; }     //unique id for each elevator car
        public int CurrentFloor { get; set; }       //floor where car currently is. Resets each time elevator moves to new floor.
        public bool IsMoving { get; set; }      //true if car is moving. False if car is stationary.
        public bool IsMovingUp { get; set; }        //only true if IsMoving == true AND car is moving to higher floor number
                                                    //false if IsMoving == false
                                                    //false if IsMoving == true AND car is moving to lower floor number
        public bool DoorIsOpen { get; set; }        //checks if door is open



        // Button Panel
        // Door

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
    }
}