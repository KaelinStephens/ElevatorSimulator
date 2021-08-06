using System.Collections.Generic;
using System.Linq;
using ElevatorFinalProject.Domain.Models;
using ElevatorFinalProject.Domain.Services;
using FluentAssertions;
using Xunit;
using static ElevatorFinalProject.Domain.Models.DirectionEnum;

namespace ElevatorTests
{
    public class CallCommandServiceTests
    {
        
        [Theory, AutoMoqData]
        public void Add_TwoInputs_TwoItemsInQueue(CallCommand a, CallCommand b, CallCommandService s)
        {
            // Arrange
            

            // Act
            CallCommandService.Commands.Add(a);
            CallCommandService.Commands.Add(b);

            // Assert
            var result = CallCommandService.Commands;
            CallCommandService.Commands.Should().Contain(x => x.Direction == a.Direction && x.Floor == a.Floor);
            CallCommandService.Commands.Should().Contain(x => x.Direction == b.Direction && x.Floor == b.Floor);
            CallCommandService.Commands.Count().Should().Be(2);

        }

    }
}
