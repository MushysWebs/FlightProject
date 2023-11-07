using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*                                              REMOVE THIS .CS FILE AND INCORPORATE LOGIC INTO THE .RAZOR FILES
namespace GroupFlightProject.Data
{

    public class Services
    {

        public List<Flight> FindFlights(string departureLocation, string destination, string day)
        {
            try
            {
                if (flights == null || !flights.Any())
                {
                    Console.WriteLine("Error: No flights available.");
                    return new List<Flight>(); // Return an empty list or handle the situation as needed
                }

                return flights.Where(f => f.DepartureLocation.Equals(departureLocation, StringComparison.OrdinalIgnoreCase) &&
                    f.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase) &&
                    f.Day.Equals(day, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding flights: {ex.Message}");
                // Handle the exception or log the error
                return new List<Flight>(); // Return an empty list or handle the situation as needed
            }
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
        }
    }
}*/
