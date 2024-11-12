namespace Models;

public class Vehicle
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Model { get; set; } // سال ساخت
    public string? CurrentKilometer { get; set; }
    public VehicleType Type { get; set; }
    public List<MaintenanceData> MaintenanceData { get; set; } = new List<MaintenanceData>();
}

public enum VehicleType
{
    Cat,
    MotorCycle,
    Bicycle
}