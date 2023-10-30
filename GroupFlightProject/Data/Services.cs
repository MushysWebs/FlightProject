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
        public Services()
        {
            FlightCsvLoader flightLoader = new FlightCsvLoader();
            flights = flightLoader.LoadFlights("GroupFlightProject/flights.csv").ToList();
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
}
