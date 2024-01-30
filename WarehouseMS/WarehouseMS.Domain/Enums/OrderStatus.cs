namespace WarehouseMS.Domain.Enums;

public enum OrderStatus
{
    ReadyToShip,
    InTransit,
    Delivered,
    Cancelled,
    Returned,
    Delayed,
    Completed,
    OnHold,
    AwaitingPickup
}