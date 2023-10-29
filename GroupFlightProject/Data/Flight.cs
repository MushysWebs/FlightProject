using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupFlightProject.Data
{
    public class Flight
    {
        public string FlightCode { get; set; }
        public string Airline { get; set; }
        public string DepartureLocation { get; set; }
        public string Destination { get; set; }
        public string Day {  get; set; }
        public string Time { get; set; }
        public int AvailableSeats { get; set; }
        public int FlightNumber { get; set; }
    }
}
