using System;

namespace ElevatorFinalProject.Domain.Models
{
    public class CallCommand  : IEquatable<CallCommand> //This is a class of commands which call the elevator to a certain floor going a certain direction.
    {                               //This is NOT a class of buttons or a button panel. These are just the call requests which are put into
                                    //the elevator's hashset. 
        public DirectionEnum Direction { get; set; }
        public int Floor { get; internal set; }

        public CallCommand(DirectionEnum direction, int floor)
        {
            Floor = floor;
            Direction = direction;
        }
        public bool Equals(CallCommand other)
        {
            if (this.Direction == other.Direction && this.Floor == other.Floor)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return Direction.GetHashCode() + Floor.GetHashCode();
        }
    }
}