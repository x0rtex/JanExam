namespace JanExam;

public class Event(string name, DateTime eventDate, EventType typeOfEvent) : IComparable<Event>
{
    public string Name { get; set; } = name;
    public DateTime EventDate { get; set; } = eventDate;
    public List<Ticket> Tickets { get; set; } = [];
    public EventType TypeOfEvent { get; set; } = typeOfEvent;

    public int CompareTo(Event? other) => EventDate.CompareTo(other?.EventDate);

    public override string ToString() => $"{Name} - {EventDate.ToShortDateString()}";
}
