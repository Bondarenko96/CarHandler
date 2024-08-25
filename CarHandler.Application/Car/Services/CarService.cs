using Microsoft.EntityFrameworkCore;

public class CarService(ApplicationDbContext context)
{
    public async Task<Car> CreateAsync(CreateCarDto dto, CancellationToken cancellationToken)
    {
        var employee = context.Employees.SingleOrDefaultAsync(x=> x.Id == dto.EmployeeId);
        
        if(employee == null)
            throw new ArgumentException("Сотрудник с таким ID не существует.");

        if(context.Cars.Any(x => x.LicensePlate == dto.LicensePlate))
            throw new ArgumentException("Такой номер уже есть в базе.");

        var car = Car.Create(dto.Id, dto.LicensePlate, dto.Make, dto.EmployeeId);

        await context.Cars.AddAsync(car, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return car;
    }

    public async Task<Car?> GetAsync(GetCarDto dto, CancellationToken cancellationToken)
    {
        return await context.Cars.Where(x => x.Id == dto.Id).SingleOrDefaultAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var car = await context.Cars.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (car == null)
            throw new ArgumentException("Машина с таким ID не найдена.");

        context.Cars.Remove(car);
        await context.SaveChangesAsync(cancellationToken);
    }


    public async Task<Car> UpdateAsync(UpdateCarDto dto, CancellationToken cancellationToken)
    {
        var car = await context.Cars.SingleOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (car == null)
            throw new ArgumentException("Машина с таким ID не найдена.");

        car.EmployeeId = dto.EmployeeId;
        car.LicensePlate = dto.LicensePlate;
        car.Make = dto.Make;

        context.Entry(car).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        return car;
    }


    public bool Check(string licensePlate)
    {
        return context.Cars.Any(x => x.LicensePlate == licensePlate);
    }
}