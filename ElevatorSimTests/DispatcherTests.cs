using System;
using ElevatorSim;
using Xunit;

namespace ElevatorSimTests
{
    public class DispatcherTests
    {
        [Fact]
        public void TestInitializeDispatcher()
        {
            //Arrange
            var dispatcher = new Dispatcher();
            //Act
            var checkItStarts = Dispatcher.InitializeDispatcher();
            //Assert
            Assert.True(checkItStarts);
        }

        [Fact]
        public void TestStopDispatcher()
        {
            //Arrange
            var dispatcher = new Dispatcher();
            //Act
            var checkItStops = Dispatcher.StopDispatcher();
            //Assert
            Assert.False(checkItStops);
        }

        [Fact]
        public void GetNearestAvailableElevatorDoesSomething()
        {
            //Arrange
            var dispatcher = new Dispatcher();
            var elev1 = new ElevatorCar() {ElevatorID = 0, CurrentFloor = 5, IsMoving = false};
            ElevatorSystem.AllElevators.Add(elev1);
            ElevatorSystem.AllFloors.Add(0);
            ElevatorSystem.AllFloors.Add(1);
            ElevatorSystem.AllFloors.Add(2);
            ElevatorSystem.AllFloors.Add(3);
            ElevatorSystem.AllFloors.Add(4);
            ElevatorSystem.AllFloors.Add(5);
            ElevatorSystem.AllFloors.Add(6);
            //Act
            bool checkItDoesSomething = Dispatcher.GetNearestAvailableElevator(2, false) != null;
            //Assert
            Assert.True(checkItDoesSomething);
        }
        // [Theory]
        // []
        // public void GetNearestAvailableElevator()
        // {
        //     //Arrange
        //     var dispatcher = new Dispatcher();
        //     //Act
        //     var checkItStops = Dispatcher.GetNearestAvailableElevator();
        //     //Assert
        //     Assert.False(checkItStops);
       // }
    }
}
