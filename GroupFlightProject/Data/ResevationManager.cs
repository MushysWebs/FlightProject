using Microsoft.Extensions.Logging;
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

    public string Status
    {
        get => IsActive ? "Active" : "Inactive";
        set => IsActive = value == "Active";
    }

    public override string ToString()
    {
        return $"Reservation Code: {ReservationCode}, Flight: {FlightCode}, Airline: {Airline}, Cost: {Cost}, Name: {Name}, Citizenship: {Citizenship}, Status: {Status}";
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
    private void LoadReservations()
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
            Console.WriteLine($"Could not save file. {ex.Message}");
        }
    }

    public List<Reservation> FindReservations(string reservationCode, string airline, string name)
    {
        return string.IsNullOrWhiteSpace(reservationCode) &&
               string.IsNullOrWhiteSpace(airline) &&
               string.IsNullOrWhiteSpace(name)
            ? Reservations
            : Reservations.Where(r =>
                (string.IsNullOrWhiteSpace(reservationCode) || r.ReservationCode.Equals(reservationCode, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(airline) || r.Airline.Contains(airline, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(name) || r.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            ).ToList();
    }


    public void UpdateReservation(Reservation updatedReservation)
    {
        // Find and update the reservation in the list
        var reservation = Reservations.FirstOrDefault(r => r.ReservationCode == updatedReservation.ReservationCode);
        if (reservation != null)
        {
            reservation.Name = updatedReservation.Name;
            reservation.Citizenship = updatedReservation.Citizenship;
            reservation.IsActive = updatedReservation.Status == "Active";
        }
    }

    public void Persist()
    {
        string json = JsonSerializer.Serialize(Reservations);
        File.WriteAllText(filePath, json);
    }

    public string GenerateReservationCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numbers = "0123456789";
        var random = new Random();
        return $"{chars[random.Next(chars.Length)]}{new string(Enumerable.Repeat(numbers, 4).Select(s => s[random.Next(s.Length)]).ToArray())}";
    }

}