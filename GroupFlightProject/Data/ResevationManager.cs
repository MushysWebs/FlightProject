﻿/*  Names: Ricky To, Grady Spurril, Logan Hoppen
    Date: November 14th 2023
    Description:    This code file defines two classes, Reservation and ReservationManager, 
                    used to manage flight reservations.
                    Reservation class handles the data for a flight reservation.
                    ReservationManager handles the loading, updating, and persisting of reservations in a JSON file.
                    It provides functionality to find reservations based on search criteria, update reservation details,
                    and generates reservation codes.
 */

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

// a single flight reservation with its details
public class Reservation
{
    public string ReservationCode { get; set; }
    public string FlightCode { get; set; }
    public string Airline { get; set; }
    public string Cost { get; set; }
    public string Name { get; set; }
    public string Citizenship { get; set; }
    public bool IsActive { get; set; }

    // Property to get/set the status of the reservation as "Active" or "Inactive"
    public string Status
    {
        get => IsActive ? "Active" : "Inactive";
        set => IsActive = value == "Active";
    }

    // Overrides ToString() to provide a string representation of the reservation
    public override string ToString()
    {
        return $"Reservation Code: {ReservationCode}, Flight: {FlightCode}, Airline: {Airline}, Cost: {Cost}, Name: {Name}, Citizenship: {Citizenship}, Status: {Status}";
    }
}

// Manages the reservations and interactions with the storage file.
public class ReservationManager
{
    // List to hold all reservations
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    // File path to store reservations (Local Appdata)
    public string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Traveless", "reservations.json");

    // initialize the ReservationManager and load existing reservations
    public ReservationManager()
    {
        // Ensure the directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        // Load existing reservations
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

    // Finds reservations based on the search criteria
    public List<Reservation> FindReservations(string reservationCode, string airline, string name)
    {
        // Return all reservations if no criteria specified. otherwise, filter the list
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

    // Updates a reservation with new details
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

    // Persists the current state of reservations to the JSON file
    public void Persist()
    {
        string json = JsonSerializer.Serialize(Reservations);
        File.WriteAllText(filePath, json);
    }

    // Generates a new reservation code
    public string GenerateReservationCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numbers = "0123456789";
        var random = new Random();
        return $"{chars[random.Next(chars.Length)]}{new string(Enumerable.Repeat(numbers, 4).Select(s => s[random.Next(s.Length)]).ToArray())}";
    }

}