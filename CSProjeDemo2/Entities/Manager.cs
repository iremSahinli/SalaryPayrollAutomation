using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSProjeDemo2.Entities;

public class Manager : Employee
{

    public static decimal Bonus = 1000;
    private const int MinHourlyWage = 500;
    public Manager(string fullName, string title, decimal hourlyWage, int workHours) : base(fullName, title, hourlyWage, workHours)
    {
    }

    public override decimal CalculateSalary()
    {
        if (HourlyWage < MinHourlyWage)
        {
            Console.WriteLine("Yöneticilerin saatlik ücreti 500₺'dir, daha düşük olamaz!");
            HourlyWage = MinHourlyWage;
        }
        var Salary = HourlyWage * WorkHours + Bonus;
        return Salary;
    }
}
