namespace JanExam;

public class Ticket(string name, decimal price, int availableTickets)
{
    public string Name { get; set; } = name;
    public decimal Price { get; set; } = price;
    public int AvailableTickets { get; set; } = availableTickets;

    public override string ToString()
    {
        if (AvailableTickets <= 0)
            return $"{Name} - {Price:c} [UNAVAILABLE]";

        return $"{Name} - {Price:c} [AVAILABLE - {AvailableTickets}]";
    } 
}
