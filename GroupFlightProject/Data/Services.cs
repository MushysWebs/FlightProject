using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupFlightProject.Data
{

    public class Services
    {
        private List<Flight> flights;
        private FlightCsvLoader flightLoader;
        public Services()
        {
            flightLoader = new FlightCsvLoader();
            LoadFlightsAsync();
        }

        private async Task LoadFlightsAsync()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("flights.csv");
            using var reader = new StreamReader(stream);

            var csvContents = await reader.ReadToEndAsync();

            flights = flightLoader.LoadFlightsFromCsv(csvContents);
        }

        public List<Flight> FindFlights(string departureLocation, string destination, string day)
        {
            return flights.Where(f => f.DepartureLocation.Equals(departureLocation, StringComparison.OrdinalIgnoreCase) &&
            f.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase) &&
            f.Day.Equals(day, StringComparison.OrdinalIgnoreCase))
            .ToList();
        }

        public Reservation MakeReservation(Flight pickedFlight, string name, string citizenship)
        {
            if (pickedFlight == null || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(citizenship))
            {
                throw new ArgumentException("Invalid input.");
            }
            if (pickedFlight.AvailableSeats > 0)
            {
                pickedFlight.AvailableSeats--;

                Reservation newReservation = new Reservation
                {
                    //implement code generation method
                    //flight code
                    //airline
                    //cost
                    //name
                    Citizenship = citizenship,
                    IsActive = true
                };
                //save reservation method
                return newReservation;
            }

            else
            {
                throw new InvalidOperationException("No more seats availible.");
            }
        }

        private string GenerateReservationCode()
        {
            // implement reservation code logic
            return "code";
        }

        private long CalculateCost()
        {
            // implement cost calculation logic. remember to $format
            return 0;
        }

        /*private void SaveReservation(Reservation reservation)
        {
            METHOD FOR SAVING RESERVATIONS
        }*/

        /*private void ModifyReservation(Reservation reservation)
        {
            METHOD FOR MODIFYING RESERVATIONS
        }*/
    }
    public class FlightCsvLoader
    {
        public List<Flight> LoadFlightsFromCsv(string csvContents)
        {
            var flights = new List<Flight>();

            string[] lines = csvContents.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                var values = line.Split(',');

                if (values.Length >= 8)
                {
                    Flight newFlight = new Flight
                    {
                        FlightCode = values[0],
                        Airline = values[1],
                        DepartureLocation = values[2],
                        Destination = values[3],
                        Day = values[4],
                        Time = values[5],
                        AvailableSeats = int.Parse(values[6]),
                        FlightNumber = int.Parse(values[7])
                    };

                    flights.Add(newFlight);
                }
            }

            return flights;
        }
    }
}


