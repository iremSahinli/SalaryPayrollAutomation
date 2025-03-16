using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSProjeDemo2.Entities;

public abstract class Employee
{
    public string FullName { get; set; }
    public string Title { get; set; }
    public decimal HourlyWage { get; set; }
    public int WorkHours { get; set; }

    public Employee()
    {

    }

    public Employee(string fullName, string title, decimal hourlyWage, int workHours)
    {
        FullName = fullName;
        Title = title;
        HourlyWage = hourlyWage;
        WorkHours = workHours;
    }

    public abstract decimal CalculateSalary();
}
