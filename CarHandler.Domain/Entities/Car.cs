public class Car : BaseEntity
{    
    private Car() { }
    private Car(int id, string licensePlate, string make, int employeeId)
    {
        Id = id;
        LicensePlate = licensePlate;
        Make = make;
        EmployeeId = employeeId;
        CreationTime = DateTime.UtcNow;
    }
    public string LicensePlate { get; set; } = "";
    public string Make { get; set; } = "";
    public int EmployeeId { get; set; }
    public Employee Employee { get; private set; } = default!;

    public static Car Create(int id, string licensePlate, string make, int employeeId)
    {
        return new Car(id, licensePlate, make, employeeId);
    }
}