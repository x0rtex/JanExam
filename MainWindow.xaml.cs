using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
namespace JanExam;

/// <summary>
/// https://github.com/x0rtex/JanExam
/// </summary>
public partial class MainWindow : Window
{
    // List of all events
    public List<Event> Events { get; set; } = [];

    // List of events that are currently visible
    // ObservableCollection automatically shows changes in the list
    public ObservableCollection<Event> EventsVisible { get; set; } = [];

    // Main window where objects are initialized
    public MainWindow()
    {
        InitializeComponent();

        Event event1 = new("Oasis Croke Park", new DateTime(2025, 06, 20), EventType.Music);
        Event event2 = new("Electric Picnic", new DateTime(2025, 08, 20), EventType.Music);
        event1.Tickets.Add(new Ticket("Early Bird", 100m, 100));
        event1.Tickets.Add(new VipTicket("Ticket and Hotel Package", 150m, 100m, "4* Hotel", 100));
        event2.Tickets.Add(new Ticket("Platinum", 150m, 100));
        event2.Tickets.Add(new VipTicket("Weekend Ticket", 200m, 100m, "With Camping", 100));
        Events = [event1, event2];

        LbxEvents.ItemsSource = EventsVisible;
        FilterEvents();
    }

    // Called when ticket list needs to change or be hidden
    private void RefreshTickets()
    {
        LbxTickets.ItemsSource = null;

        Event selectedEvent = (Event)LbxEvents.SelectedItem;
        if (selectedEvent is not null)
            LbxTickets.ItemsSource = selectedEvent.Tickets;
    }

    // Refresh tickets when deselecting event or selecting new event
    private void LbxEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        RefreshTickets();
    }

    // Called when purchasing tickets, performs validations and updates ticket availability
    private void BtnBookTickets_Click(object sender, RoutedEventArgs e)
    {
        Ticket selectedTicket = (Ticket)LbxTickets.SelectedItem;

        if (selectedTicket is null)
        {
            MessageBox.Show("Please select a ticket");
            return;
        }

        if (!int.TryParse(TbxNumberOfTickets.Text, out int numberOfTickets) || numberOfTickets <= 0)
        {
            MessageBox.Show("Please enter a valid number of tickets");
            return;
        }

        if (selectedTicket.AvailableTickets < numberOfTickets)
        {
            MessageBox.Show("Not enough tickets available");
            return;
        }

        selectedTicket.AvailableTickets -= numberOfTickets;
        decimal additionalCost = (selectedTicket is VipTicket ticket) ? ticket.AdditionalCost : 0;
        decimal totalCost = (selectedTicket.Price + additionalCost) * numberOfTickets;
        RefreshTickets();
        MessageBox.Show($"Purchased of {numberOfTickets} tickets for {totalCost:c} was successful.");
    }

    // Clear search box when clicked
    private void TbxEventsSearch_GotFocus(object sender, RoutedEventArgs e)
    {
        TbxEventsSearch.Text = "";
    }

    // Show "Search" in search box when unclicked
    private void TbxEventsSearch_LostFocus(object sender, RoutedEventArgs e)
    {
        TbxEventsSearch.Text = "Search";
    }

    // Filter events every time the search box changes
    private void TbxEventsSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        FilterEvents(TbxEventsSearch.Text);
    }

    // Filter events based on search string
    private void FilterEvents(string search = "")
    {
        EventsVisible.Clear();

        if (search == "" || search == "Search")
            foreach (Event e in Events)
                EventsVisible.Add(e);

        else
            foreach(Event e in Events)
                if (e.Name.ToLower().Contains(search.ToLower()))
                    EventsVisible.Add(e);
    }

    // Increase number of tickets when '+' clicked
    private void BtnPlus_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(TbxNumberOfTickets.Text, out int numberOfTickets))
        {
            TbxNumberOfTickets.Text = "1";
            return;
        }

        numberOfTickets++;
        TbxNumberOfTickets.Text = numberOfTickets.ToString();
    }

    // Decrease number of tickets when '-' clicked
    private void BtnMinus_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(TbxNumberOfTickets.Text, out int numberOfTickets) || numberOfTickets <= 0)
        {
            TbxNumberOfTickets.Text = "0";
            return;
        }

        numberOfTickets--;
        TbxNumberOfTickets.Text = numberOfTickets.ToString();
    }
}