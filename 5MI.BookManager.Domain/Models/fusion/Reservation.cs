using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5MI.BookManager.Domain.Models.fusion
{
    class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatedBy { get; set; }
        public string ReservationTitle { get; set; } = string.Empty;
        public ReservationType Type { get; set; }
        public virtual User User { get; set; }
    }
}
