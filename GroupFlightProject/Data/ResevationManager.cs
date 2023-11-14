using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Reservation
{
    public string ReservationCode { get; set; }
    public string FlightCode { get; set; }
    public string Airline { get; set; }
    public string Cost { get; set; }
    public string Name { get; set; }
    public string Citizenship { get; set; }
    public bool IsActive { get; set; }

    public override string ToString()
    {
        return $"Reservation Code: {ReservationCode}, Flight: {FlightCode}, Airline: {Airline}, Cost: {Cost}, Name: {Name}, Citizenship: {Citizenship}, Active: {IsActive}";
    }
}
public class ReservationManager
{
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    public string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Traveless", "reservations.json");

    public ReservationManager()
    {
        // Ensure the directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        // Load existing reservations if the file exists
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Reservations = JsonSerializer.Deserialize<List<Reservation>>(json) ?? new List<Reservation>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"No file found. {ex.Message}");
        }
    }

    public void Persist(object data)
    {
        try
        {
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, json);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Could not save file. {ex.Message}");
        }

    }

    public string GenerateReservationCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numbers = "0123456789";
        var random = new Random();
        return $"{chars[random.Next(chars.Length)]}{new string(Enumerable.Repeat(numbers, 4).Select(s => s[random.Next(s.Length)]).ToArray())}";
    }

}