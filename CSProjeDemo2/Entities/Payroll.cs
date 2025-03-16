using CSProjeDemo2.Entities;
using System.Text.Json;
using System.Text.Encodings.Web;

public static class Payroll
{
    public static void GlobalPayroll(Employee employee, string outputDirectory)
    {
        decimal salary = employee.CalculateSalary();
        decimal overtimePay = employee is Officer officer ? officer.OverTimePay : 0;

        var payrollData = new
        {
            MaasBordro = DateTime.Now.ToString("MMMM yyyy"),
            PersonelIsmi = employee.FullName,
            CalismaSaati = employee.WorkHours,
            AnaOdeme = $"₺ {employee.WorkHours * employee.HourlyWage}",
            Mesai = $"₺ {(employee is Manager ? Manager.Bonus : overtimePay)}",
            ToplamOdeme = $"₺ {salary}"
        };

        string employeeFolder = Path.Combine(outputDirectory, employee.FullName);
        Directory.CreateDirectory(employeeFolder);

        string payrollFilePath = Path.Combine(employeeFolder, $"Maas Bordro,{DateTime.Now:MM_yyyy}.json");
        string payrollJson = JsonSerializer.Serialize(payrollData, new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });

        File.WriteAllText(payrollFilePath, payrollJson);
        Console.WriteLine($"{employee.FullName} isimli personelin maaş bilgileri başarıyla kaydedildi.\n" + new string('*', 50));
    }
}
