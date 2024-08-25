using Microsoft.EntityFrameworkCore;
public class EmployeeService(ApplicationDbContext context)
{
    public async Task<Employee> CreateAsync(CreateEmployeeDto dto, CancellationToken cancellationToken)
    {
        var employee = Employee.Create(dto.Id, dto.FirstName, dto.LastName, dto.PhoneNumber);

        await context.Employees.AddAsync(employee, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return employee;
    }
    
    public async Task<Employee?> GetAsync(GetEmployeeDto dto, CancellationToken cancellationToken)
    {
        return await context.Employees.Where(x => x.Id == dto.Id).SingleOrDefaultAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var employee = await context.Employees.SingleOrDefaultAsync(x=> x.Id == id, cancellationToken);

        if(employee == null)
            throw new ArgumentException("Сотрудник с таким ID не найден.");

        context.Employees.Remove(employee);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Employee> UpdateAsync(UpdateEmployeeDto dto, CancellationToken cancellationToken)
    {
        var employee = await context.Employees.SingleOrDefaultAsync(x=> x.Id == dto.Id, cancellationToken);

        if(employee == null)
            throw new ArgumentException("Сотрудник с таким ID не найден.");

        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.PhoneNumber = dto.PhoneNumber;

        context.Entry(employee).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        return employee;
    }
}