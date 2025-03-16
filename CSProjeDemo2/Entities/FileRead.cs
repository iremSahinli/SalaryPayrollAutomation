using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSProjeDemo2.Entities;

public static class FileRead
{
    public static List<Employee> ReadEmployeeFromJson(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Dosya bulunamadı!");
        }
        string content = File.ReadAllText(filePath);
        var jsonDocument = JsonDocument.Parse(content);
        List<Employee> employees = new List<Employee>();


        foreach (var element in jsonDocument.RootElement.EnumerateArray())
        {
            if (element.TryGetProperty("Title", out JsonElement typeElement))
            {
                string title = typeElement.GetString();
                if (title == "Officer")
                {
                    employees.Add(JsonSerializer.Deserialize<Officer>(element.GetRawText()));
                }
                else if (title == "Manager")
                {
                    employees.Add(JsonSerializer.Deserialize<Manager>(element.GetRawText()));
                }
            }
            else
            {
                throw new KeyNotFoundException("The given key 'Type' was not present in the JSON data.");
            }
        }
        return employees;
    }
}
