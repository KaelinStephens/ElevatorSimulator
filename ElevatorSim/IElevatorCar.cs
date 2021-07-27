using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElevatorSim
{
    public interface IElevatorCar
    {
        HashSet<int> UpCalls { get; }
        HashSet<int> DownCalls { get; }
        int CurrentFloor { get; }
        int TotalFloors { get; }
        Task UpCallRequestAsync(int floor);
        Task DownCallRequestAsync(int floor);
        IButtonPanel GetCallPanelForFloor(int floor);
        Task StopAsync();
        Task StartAsync();
        void RegisterCallPanel(IButtonPanel newPanel);
        public bool IsMoving { get; set; }      //true if car is moving. False if car is stationary.
        public bool IsMovingUp { get; set; }        //only true if IsMoving == true AND car is moving to higher floor number
        //false if IsMoving == false
        //false if IsMoving == true AND car is moving to lower floor number
    }
}