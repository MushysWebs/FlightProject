using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupFlightProject.Data
{
    public class Reservation
    {
        public string ReservationCode { get; set; }
        public string FlightCode { get; set; }
        public string Airline { get; set; }
        public string Cost { get; set; }
        public string Name { get; set; }
        public string Citizenship { get; set; }
        public bool IsActive { get; set; }
    }
}
