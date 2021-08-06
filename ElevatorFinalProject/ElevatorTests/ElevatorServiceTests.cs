using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElevatorFinalProject.Domain.Entities;
using ElevatorFinalProject.Domain.Interfaces;
using ElevatorFinalProject.Domain.Models;
using ElevatorFinalProject.Domain.Services;
using FluentAssertions;
using Xunit;

namespace ElevatorTests
{
    public class ElevatorServiceTests
    {
        [Theory]
        [InlineData(0, 5, DirectionEnum.UP, DirectionEnum.UP)]

        public async void DetermineNewDirectionUpTest(int startFloor, int callFloor, DirectionEnum callDirection, DirectionEnum expected)
        {
            //Arrange

            ICallCommandService callCommandService = new CallCommandService();
            IEventService eventService = new EventService();
            var elevator = new Elevator(callCommandService, eventService, startFloor, callFloor + 3, 100);
            elevator.Direction = DirectionEnum.STATIONARY;
            var callCommand = new CallCommand(callDirection, callFloor);
            elevator.CallCommands.Add(callCommand);

            //Act

            var e = elevator.DetermineNewDirection();
            var actual = elevator.Direction;

            //Assert

            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(0, 5, DirectionEnum.UP, DirectionEnum.UP)]

        public async void SelectMostAppropriateDirectionBasedOnHeuristicTest(int startFloor, int callFloor, DirectionEnum callDirection, DirectionEnum expected)
        {
            //Arrange

            ICallCommandService callCommandService = new CallCommandService();
            IEventService eventService = new EventService();
            var elevator = new Elevator(callCommandService, eventService, startFloor, callFloor + 3, 100);
            elevator.Direction = DirectionEnum.STATIONARY;
            var callCommand = new CallCommand(callDirection, callFloor);
            elevator.CallCommands.Add(callCommand);

            //Act

            var actual = elevator.SelectMostAppropriateDirectionBasedOnHeuristic(startFloor, elevator.TopFloor, elevator.CallCommands);
            

            //Assert

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 5, DirectionEnum.UP, DirectionEnum.UP)]

        public async void GetClosestDirectionWhereCallAndDirectionAreSameTest(int startFloor, int callFloor, DirectionEnum callDirection, DirectionEnum expected)
        {
            //Arrange

            ICallCommandService callCommandService = new CallCommandService();
            IEventService eventService = new EventService();
            var elevator = new Elevator(callCommandService, eventService, startFloor, callFloor + 3, 100);
            elevator.Direction = DirectionEnum.STATIONARY;
            var callCommand = new CallCommand(callDirection, callFloor);
            elevator.CallCommands.Add(callCommand);

            //Act

            var actual = elevator.GetClosestDirectionWhereCallAndDirectionAreSame(startFloor, elevator.TopFloor, elevator.CallCommands);


            //Assert

            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData(0, 5, DirectionEnum.UP, 5)]

        public async void GetClosestUpcallAboveCurrentFloorTest(int startFloor, int callFloor, DirectionEnum callDirection, int? expected)
        {
            //Arrange

            ICallCommandService callCommandService = new CallCommandService();
            IEventService eventService = new EventService();
            var elevator = new Elevator(callCommandService, eventService, startFloor, callFloor + 3, 100);
            elevator.Direction = DirectionEnum.STATIONARY;
            var callCommand = new CallCommand(callDirection, callFloor);
            elevator.CallCommands.Add(callCommand);

            //Act

            var actual = elevator.GetClosestUpcallAboveCurrentFloor(startFloor, elevator.TopFloor, elevator.CallCommands);


            //Assert

            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData(0, 5, DirectionEnum.UP, null)]

        public async void GetClosestDowncallBelowCurrentFloorTest(int startFloor, int callFloor, DirectionEnum callDirection, int? expected)
        {
            //Arrange

            ICallCommandService callCommandService = new CallCommandService();
            IEventService eventService = new EventService();
            var elevator = new Elevator(callCommandService, eventService, startFloor, callFloor + 3, 100);
            elevator.Direction = DirectionEnum.STATIONARY;
            var callCommand = new CallCommand(callDirection, callFloor);
            elevator.CallCommands.Add(callCommand);

            //Act

            var actual = elevator.GetClosestDowncallBelowCurrentFloor(startFloor, elevator.TopFloor, elevator.CallCommands);


            //Assert

            Assert.Equal(expected, actual);
        }
        // public async void ElevatorService_AddUpCall_MoveToFloorAndStop(int callFloor)
        // {
        //     // Arrange
        //     var eventService = new TestEventService(0);
        //     var commandService = new CallCommandService();
        //     var elevatorService = new Elevator(commandService, eventService, 0, 10, 10);
        //     var task1 = elevatorService.StartAsync();
        //
        //     // Act
        //     CallCommandService.Commands.Add(new CallCommand(DirectionEnum.UP, callFloor% 10));
        //     await Task.WhenAll(task1);
        //     // Assert
        //     eventService.Events.Count(x => x.EventType == EventType.MOVE_UP_FLOOR).Should().Be((callFloor % 10));
        //     eventService.Events.Count(x => x.EventType == EventType.DOOR_CLOSE).Should().Be(1);
        //     eventService.Events.Count(x => x.EventType == EventType.DOOR_OPEN).Should().Be(1);
        //     eventService.Events.Count(x => x.EventType == EventType.MOVE_DOWN_FLOOR).Should().Be(0);
        //
        //     eventService.Events.Count(x => x.EventType == EventType.ARRIVAL).Should().Be(1);
        //     eventService.CurrentFloor.Should().Be(callFloor % 10);
        // }
        //
        // [Theory]
        // [InlineData(0)]
        // [InlineData(5)]
        // [InlineData(9)]
        // public void ElevatorService_AddDownCall_MoveToFloorAndStop(int callFloor)
        // {
        //     // Arrange
        //     var eventService = new TestEventService(10);
        //     var commandService = new CallCommandService();
        //     var elevatorService = new Elevator(commandService, eventService, 10, 10, timerInterval: 5, 0);
        //     elevatorService.StartAsync();
        //
        //     // Act
        //     CallCommandService.Commands.Add(new CallCommand(DirectionEnum.UP, callFloor));
        //
        //     // Assert
        //     eventService.Events.Count(x => x.EventType == EventType.MOVE_UP_FLOOR).Should().Be(0);
        //     eventService.Events.Count(x => x.EventType == EventType.DOOR_CLOSE).Should().Be(1);
        //     eventService.Events.Count(x => x.EventType == EventType.DOOR_OPEN).Should().Be(1);
        //     eventService.Events.Count(x => x.EventType == EventType.MOVE_DOWN_FLOOR).Should().Be(callFloor - 1);
        //     eventService.Events.Count(x => x.EventType == EventType.ARRIVAL).Should().Be(1);
        //     eventService.CurrentFloor.Should().Be(callFloor);
        // }

        // public class TestEventService : IEventService
        // {
        //     public int CurrentFloor;
        //     public List<Event> Events = new List<Event>();
        //
        //     public TestEventService(int startFloor)
        //     {
        //         CurrentFloor = startFloor;
        //     }
        //
        //     public IDisposable Subscribe(IObserver<Event> observer) => new Unsubscriber<Event>(null, observer);
        //
        //     public void AddEvent(string message = "Hi there", EventType type = EventType.ELEVATOR_BUTTON_PANEL_LIT)
        //     {
        //         switch (type)
        //         {
        //             case EventType.MOVE_UP_FLOOR:
        //                 CurrentFloor++;
        //                 break;
        //             case EventType.MOVE_DOWN_FLOOR:
        //                 CurrentFloor--;
        //                 break;
        //         }
        //         Events.Add(new Event(message, type));
        //     }
        // }
    }


}