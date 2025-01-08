using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanExam
{
    internal class VipTicket(string name, decimal price, decimal additionalCost, string additionalExtras, int availableTickets) 
        : Ticket(name, price, availableTickets)
    {
        public decimal AdditionalCost = additionalCost;
        public string AdditionalExtras = additionalExtras;
    }
}
