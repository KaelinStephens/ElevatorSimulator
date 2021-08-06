namespace ElevatorFinalProject.Domain.Models
{
    public enum EventType
    {
        DOOR_OPEN, DOOR_CLOSE, ARRIVAL, MOVE_UP_FLOOR, MOVE_DOWN_FLOOR, UP_BUTTON_CALL, DOWN_BUTTON_CALL,
        ELEVATOR_BUTTON_PANEL_LIT,
        ELEVATOR_BUTTON_PANEL_NOT_LIT
    }
}