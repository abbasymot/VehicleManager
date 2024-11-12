namespace Models;

public class MaintenanceData
{
    public int Id { get; set; }
    public VehiclePart VehiclePart { get; set; }
    public DateTime DateChanged { get; set; }
    public int ChangedKilometer { get; set; }
    public int ReminderKilometer { get; set; }
}

public enum VehiclePart
{
    EngineOil,
    OilFilter,
    AirFilter,
    BreakFluid,
    BreakPads,
    SparkPlugs,
    Battery,
    Coolant, // ضد یخ
    Tiers,
    ChainAndSprockets,
    ForkOil
}