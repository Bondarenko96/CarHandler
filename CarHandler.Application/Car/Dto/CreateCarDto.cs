public class CreateCarDto : BaseDto
{
    public string LicensePlate { get; set; } = "";
    public string Make { get; set; } = "";
    public int EmployeeId { get; set; }
}