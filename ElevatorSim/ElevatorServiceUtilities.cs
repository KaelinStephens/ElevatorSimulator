namespace ElevatorSim
{
    public class ElevatorServiceUtilities
    {
        private readonly IElevatorCar elevatorCar;

        public ElevatorServiceUtilities(IElevatorCar elevatorCar)
        {
            this.elevatorCar = elevatorCar;
        }
    }
}