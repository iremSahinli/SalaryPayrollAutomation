using CSProjeDemo2.Entities;

Console.OutputEncoding = System.Text.Encoding.UTF8;
string filePath = "employee.json"; // JSON dosya yolu
string outputDirectory = @"C:\Users\pc\source\repos\CSProjeDemo2\CSProjeDemo2\Bordro"; // Çıktı klasörü

try
{
    decimal hourlyWage = 0;
    int workHours = 0;

    List<Employee> employees = FileRead.ReadEmployeeFromJson(filePath);

    Console.WriteLine("*********** Personel Maaş Bilgi Sistemi ***********\n" + new string('-', 50));
    foreach (var employee in employees)
    {
        Console.WriteLine($"Personel İsmi: {employee.FullName}\nÜnvan: {employee.Title}");

        ValidateEntryControls(out hourlyWage, out workHours); //Girilen verilerin doğruluğunu kontrol etme.
        DegreeControl(employee);

        // Maaşı hesaplama ve bordroyu kaydetme
        employee.HourlyWage = hourlyWage;
        employee.WorkHours = workHours;
        Payroll.GlobalPayroll(employee, outputDirectory);
    }

    // Çıktı klasöründeki JSON dosyalarını yazdırma
    Console.WriteLine("\n*********** Personel Bordro Bilgileri ***********\n" + new string('-', 50));
    PrintPayrollFiles(outputDirectory);

    // 150 saatten az çalışan var ise ekrana yazdır.
    GetEmployeesWorkLessThan150Hours(employees);
}
catch (Exception ex)
{
    Console.WriteLine($"Hata: {ex.Message}");
}


static void ValidateEntryControls(out decimal hourlyWage, out int workHours)
{
    // Kullanıcıdan veri alma
    while (true)
    {
        Console.Write("Saatlik ücreti giriniz: ");
        if (decimal.TryParse(Console.ReadLine(), out hourlyWage) && hourlyWage > 0) break;
        Console.WriteLine("Geçersiz giriş, lütfen doğru formatta tekrar deneyin!");
    }
    while (true)
    {
        Console.Write("Çalışma saatini giriniz: ");
        if (int.TryParse(Console.ReadLine(), out workHours) && hourlyWage > 0) break;
        Console.WriteLine("Geçersiz veri girişi, lütfen boşluk, harf ve noktalama işaretleri olmadan 0 dan büyük bir rakamsal değer giriniz!");
    }
}

static void GetEmployeesWorkLessThan150Hours(List<Employee> employees)
{
    var lowWorkHoursEmployees = employees.Where(e => e.WorkHours < 150).ToList();
    if (lowWorkHoursEmployees.Any())
    {
        Console.WriteLine("<- 150 saatten az çalışmış personeller: ->\n" + new string('-', 50));
        foreach (var employee in lowWorkHoursEmployees)
        {
            Console.WriteLine($"{employee.FullName} -> Toplam Saat: {employee.WorkHours}");
        }
    }
    else
    {
        Console.WriteLine("150 saatten az çalışmış personel yok.");
    }
}

static void DegreeControl(Employee employee)
{
    if (employee is Officer officer)
    {
        int degree;
        while (true)
        {
            Console.Write("Memurun derecesini giriniz: ");
            if (int.TryParse(Console.ReadLine(), out degree))
            {
                officer.Degree = degree;
                break;
            }
            Console.WriteLine("Geçersiz giriş, tekrar deneyin!");
        }
    }
}

static void PrintPayrollFiles(string outputDirectory)
{
    if (Directory.Exists(outputDirectory))
    {
        string[] jsonFiles = Directory.GetFiles(outputDirectory, "*.json", SearchOption.AllDirectories);
        if (jsonFiles.Length == 0)
        {
            Console.WriteLine("Bordro klasörü boş veya veri yok!");
        }
        else
        {
            foreach (var file in jsonFiles)
            {
                Console.WriteLine(new string('*', 50));
                string jsonContent = File.ReadAllText(file);
                Console.WriteLine(jsonContent);
            }
        }
    }
    else
    {
        Console.WriteLine("Klasör bulunamadı!");
    }
}