using System.Collections.Generic;
using ElevatorFinalProject.Domain.Models;

namespace ElevatorFinalProject.Domain.Entities
{
    public static class ElevatorExtensionMethods
    {
        public static bool IsThereAnUpcallBelowCurrentFloor(this Elevator e, int currentFloor, int totalFloors, HashSet<CallCommand> callCommands)
        {
            if (currentFloor == 0) return false;
            for (var i = currentFloor -1; i >= 0; i--)
            {
                var call = new CallCommand(DirectionEnum.UP, i);
                if (callCommands.Contains(call)) return true;
            }
            return false;
        }
        public static bool IsThereADowncallBelowCurrentFloor(this Elevator e, int currentFloor, int totalFloors, HashSet<CallCommand> callCommands)
        {
            if (currentFloor == 0) return false;
            for (var i = currentFloor - 1; i >= 1; i--)
            {
                var call = new CallCommand(DirectionEnum.DOWN, i);
                if (callCommands.Contains(call)) return true;
            }
            return false;
        }
        public static bool IsThereAnUpcallAboveCurrentFloor(this Elevator e, int currentFloor, int totalFloors, HashSet<CallCommand> callCommands)
        {
            if (currentFloor == totalFloors) return false;
            for (var i = currentFloor + 1; i <= totalFloors; i++)
            {
                var call = new CallCommand(DirectionEnum.UP, i);
                if (callCommands.Contains(call)) return true;
            }
            return false;
        }
        public static bool IsThereADowncallAboveCurrentFloor(this Elevator e, int currentFloor, int totalFloors, HashSet<CallCommand> callCommands)
        {
            if (currentFloor == totalFloors) return false;
            for (var i = currentFloor + 1; i <= totalFloors; i++)
            {
                var call = new CallCommand(DirectionEnum.DOWN, i);
                if (callCommands.Contains(call)) return true;

            }
            return false;
        }
        public static int? GetClosestUpcallAboveCurrentFloor(this Elevator e, int currentFloor, int totalFloors, HashSet<CallCommand> callCommands)
        {
            for (var i = currentFloor + 1; i <= totalFloors; i++)
            {
                var call = new CallCommand(DirectionEnum.UP, i);
                if (callCommands.Contains(call)) return i;
            }
            return null;
        }
        public static int? GetClosestDowncallBelowCurrentFloor(this Elevator e, int currentFloor, int totalFloors, HashSet<CallCommand> callCommands)
        {
            for (var i = currentFloor - 1; i >= 0; i--)
            {
                var call = new CallCommand(DirectionEnum.DOWN, i);
                if (callCommands.Contains(call)) return i;
            }
            return null;
        }
        public static int? GetFurthestDowncallAboveCurrentFloor(this Elevator e, int currentFloor, int totalFloors, HashSet<CallCommand> callCommands)
        {
            for (var i = totalFloors; i > currentFloor; i--)
            {
                var call = new CallCommand(DirectionEnum.DOWN, i);
                if (callCommands.Contains(call)) return i;
            }
            return null;
        }
        public static int? GetFurthestUpcallBelowCurrentFloor(this Elevator e, int currentFloor, int totalFloors, HashSet<CallCommand> callCommands)
        {
            for (var i = 0; i <currentFloor; i++)
            {
                var call = new CallCommand(DirectionEnum.UP, i);
                if (callCommands.Contains(call)) return i;
            }
            return null;
        }
        public static DirectionEnum GetClosestDirectionWhereCallAndDirectionAreSame(this Elevator e, int currentFloor, int totalFloors, HashSet<CallCommand> callCommands)
        {
            var closestUpcallAboveCurrentFloor = GetClosestUpcallAboveCurrentFloor(e,  currentFloor,  totalFloors,  callCommands);
            var closestDowncallBelowCurrentFloor = GetClosestDowncallBelowCurrentFloor(e, currentFloor, totalFloors, callCommands);
            var distanceUp = closestUpcallAboveCurrentFloor - currentFloor;
            var distanceDown = currentFloor - closestDowncallBelowCurrentFloor;

            // If there are no upcalls above or downcalls below we select nothing
            if (closestDowncallBelowCurrentFloor == null && closestUpcallAboveCurrentFloor == null)
            {

                return DirectionEnum.STATIONARY;
            }

            // If there are both upcalls and downcalls --attempt to choose the closest option
            if (closestDowncallBelowCurrentFloor != null && closestUpcallAboveCurrentFloor != null)
            {
                switch (distanceUp <= distanceDown)
                {
                    case true:
                        return DirectionEnum.UP;
                    case false:
                        return DirectionEnum.DOWN;
                }
            }

            // If there is only one choice, choose it
            if (closestDowncallBelowCurrentFloor != null || closestUpcallAboveCurrentFloor != null)
            {
                switch (closestDowncallBelowCurrentFloor != null)
                {
                    case (true):
                        return DirectionEnum.DOWN;
                    case (false):
                        return DirectionEnum.UP;
                }
            }

            return DirectionEnum.UP;
        }
        public static DirectionEnum GetFurthestDirectionWhereCallAndDirectionAreDifferent(this Elevator e, int currentFloor, int totalFloors, HashSet<CallCommand> callCommands)
        {
            var furthestDowncall = GetFurthestDowncallAboveCurrentFloor(e, currentFloor, totalFloors, callCommands);
            var furthestUpcall = GetFurthestUpcallBelowCurrentFloor(e, currentFloor, totalFloors, callCommands);
            var distanceToUpcall = currentFloor - furthestUpcall;
            var distanceToDowncall = furthestDowncall - currentFloor;
            switch (furthestUpcall == null, furthestDowncall == null)
            {
                // If there are no upcalls below or downcalls above we select nothing
                case (true, true):
                    return DirectionEnum.STATIONARY;
                // If there is only one choice, choose it
                case (true, false):
                    return DirectionEnum.UP;
                case (false, true):
                    return DirectionEnum.DOWN;
                // If there are both upcalls and downcalls --attempt to choose the farthest option (most economical choice)
                default:
                    switch (distanceToUpcall >= distanceToDowncall)
                    {
                        case true:
                            return DirectionEnum.DOWN;
                        case false:
                            return DirectionEnum.UP;
                    }
            }
        }
        public static DirectionEnum SelectMostAppropriateDirectionBasedOnHeuristic(this Elevator e, int currentFloor, int totalFloors, HashSet<CallCommand> callCommands)
        {
            var directionValue = GetClosestDirectionWhereCallAndDirectionAreSame(e, currentFloor, totalFloors, callCommands);
            if (directionValue == DirectionEnum.STATIONARY)
                directionValue = GetFurthestDirectionWhereCallAndDirectionAreDifferent(e, currentFloor, totalFloors, callCommands);
            return directionValue;
        }
        public static bool IsAtNadir(this Elevator e, int currentFloor, int totalFloors, HashSet<CallCommand> callCommands)
        {
            return !IsThereADowncallBelowCurrentFloor(e, currentFloor, totalFloors, callCommands) && !IsThereAnUpcallBelowCurrentFloor(e, currentFloor, totalFloors, callCommands);
        }

        public static bool IsAtApex(this Elevator e, int currentFloor, int totalFloors, HashSet<CallCommand> callCommands)
        {
            return !IsThereADowncallAboveCurrentFloor(e, currentFloor, totalFloors, callCommands) && !IsThereAnUpcallAboveCurrentFloor(e, currentFloor, totalFloors, callCommands);
        }
    }
}