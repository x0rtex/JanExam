using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanExam
{
    public class Ticket(string name, decimal price, int availableTickets)
    {
        public string Name { get; set; } = name;
        public decimal Price { get; set; } = price;
        public int AvailableTickets { get; set; } = availableTickets;

        public override string ToString() => $"{Name} - {Price:c} [AVAILABLE - {AvailableTickets}]";
    }
}
