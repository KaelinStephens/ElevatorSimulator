using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSim
{
    class Requirements
    {
        //1) elevator should move up and down
        //2) buttons to control the car
            //a) inside elevator car on button panel
            //b) on floors to call elevator
        //3) dispatcher unit algos
        //4) open/close
        //5) limitations on car ??

        //ElevatorSystem()
            // List<Elevator> allElevators
            // List<Floors> allFloors
            
            //startElevatorSystem() --initializes the dispatcher
            //stopSystem()

        //Floor()
            // int currentFloor
            // ButtonPanel buttonPanel

            //callElevator(int currFloor, buttonPanel)

        //ElevatorCar()
            // Button Panel
            // Door

            // bool isMoving
            // bool isMovingUp
            // int currentFloor

            // moveToFloor(int floorToMoveTo)

        //interface ButtonPanel()
            // bool sendInstructionToDispatcher(int currFloor, int destFloor, bool isMovingUp)

            //floor button panel
                // bool isMovingUp
                // sendInstruction(null, currFloor, isMovingUp)

            //elevator car button panel
                // map<int,bool>floorStatus
                // sendInstruction(currFloor, destFloor(button pressed), isMovingUp) 

        //interface Door()
            // bool isOpen

            // bool closeTheDoor()
            // bool openTheDoor()

        //DispatcherUnit()
            // Request array stores indexes of floors that have been requested in ascending order and their request 'head' is the position of the current floor
            // direction represents whether the head is moving up or down
            // in direction in which head is moving, service all floors one by one
            // calculate the absolute distance of the floor from the head.
            // increment the total seek count with this distance
            // currently serviced floor position now becomes the new head position
            // go to step 3 until reach one of the ends of the disk (farthest floor requested to go to from inside car)
            // if reach the end of the disk, reverse the direction and go to step 2 until all floors in request array have been serviced


    }
}
