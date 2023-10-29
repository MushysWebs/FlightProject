using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupFlightProject.Data
{
    internal class FlightCsvLoader
    {
        public IEnumerable<Flight> LoadFlights(string csvPath) 
        { 
            var flights = new List<Flight>();
            var lines = File.ReadAllLines(csvPath);

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var values = line.Split(',');

                flights.Add(new Flight

                {
                    FlightCode = values[0],
                    Airline = values[1],
                    DepartureLocation = values[2],
                    Destination = values[3],
                    Day = values[4],
                    Time = values[5],
                    AvailableSeats = int.Parse(values[6]),
                    FlightNumber = int.Parse(values[7]),

                });
            }
            return flights;
        }
    }
}
