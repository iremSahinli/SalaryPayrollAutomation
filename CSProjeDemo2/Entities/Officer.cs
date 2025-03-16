using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSProjeDemo2.Entities;

public class Officer : Employee
{
    public int Degree { get; set; }
    public decimal OverTimePay { get; private set; }

    public Officer(string fullName, string title, decimal hourlyWage, int workHours) : base(fullName, title, hourlyWage, workHours)
    {
    }

    private const int MaxWorkHours = 180;
    private const decimal degreeBonus = 0.1m; //her derece için maaş artışı

    public override decimal CalculateSalary()
    {
        decimal wage = HourlyWage > 0 ? HourlyWage : 500; 
        if (WorkHours <= MaxWorkHours)
        {
            OverTimePay = 0;
            return WorkHours * wage + Degree * wage * degreeBonus; // Normal maaş hesaplama
        }
        else
        {
            decimal overtimeHour = WorkHours - MaxWorkHours; // 180 saati aşan fazla mesai saatleri
            OverTimePay = overtimeHour * wage * 1.5m; // Fazla mesai ücreti (1.5 kat)
            decimal salary = (MaxWorkHours * wage) + OverTimePay; // Normal maaş + fazla mesai
            decimal totalSalary = salary + (Degree * wage * degreeBonus); // Derece bonusu eklenmiş toplam maaş
            return totalSalary; // Toplam maaş
        }
    }
}
