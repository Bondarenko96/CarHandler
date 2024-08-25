public class Employee : BaseEntity
{
    private Employee() { }
    private Employee(int id, string firstName, string lastName, string phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public List<Car> Cars { get; set; } = [];

    public static Employee Create(int id, string firstName, string lastName, string phoneNumber)
    {
        return new Employee(id, firstName, lastName, phoneNumber);
    }
}