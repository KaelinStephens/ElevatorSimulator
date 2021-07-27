using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSim
{
    public static class ElevatorSystem      //ElevatorSystem()
    {
        public static List<int> AllFloors { get; set; }      // List<Floors> allFloors
        public static List<ElevatorCar> AllElevators { get; set; }       // List<Elevator> allElevators
    
        public static void StartElevatorSystem()
        {
            Dispatcher.InitializeDispatcher(); // --initializes the dispatcher
        }

        public static void StopSystem()
        {
            Dispatcher.StopDispatcher();         //stops dispatcher
        }      
    }
}
