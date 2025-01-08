// https://github.com/x0rtex/JanExam
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace JanExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Event> Events { get; set; } = [];
        public ObservableCollection<Event> EventsVisible { get; set; } = [];

        public MainWindow()
        {
            InitializeComponent();

            Event event1 = new("Oasis Croke Park", new DateTime(2025, 06, 20), EventType.Music);
            Event event2 = new("Electric Picnic", new DateTime(2025, 08, 20), EventType.Music);

            event1.Tickets.Add(new Ticket("Early Bird", 100m, 100));
            event1.Tickets.Add(new VipTicket("Ticket and Hotel Package", 150m, 100m, "4* Hotel", 100));
            event2.Tickets.Add(new Ticket("Platinum", 150m, 100));
            event2.Tickets.Add(new VipTicket("Weekend Ticket", 200m, 100m, "With Camping", 100));

            Events.Add(event1);
            Events.Add(event2);

            LbxEvents.ItemsSource = EventsVisible;
            FilterEvents();
        }

        private void LbxEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Event selectedEvent = (Event)LbxEvents.SelectedItem;

            if (selectedEvent is null)
                LbxTickets.ItemsSource = null;

            else
                LbxTickets.ItemsSource = selectedEvent.Tickets;
        }

        private void BtnBookTickets_Click(object sender, RoutedEventArgs e)
        {
            Ticket selectedTicket = (Ticket)LbxTickets.SelectedItem;

            if (selectedTicket is null)
            {
                MessageBox.Show("Please select a ticket");
                return;
            }

            if (!int.TryParse(TbxNumberOfTickets.Text, out int numberOfTickets))
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
            MessageBox.Show($"Purchased of {numberOfTickets} tickets for {totalCost:c} was successful.");
        }

        private void TbxEventsSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TbxEventsSearch.Text = "";
        }

        private void TbxEventsSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TbxEventsSearch.Text = "Search";
        }

        private void TbxEventsSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterEvents(TbxEventsSearch.Text);
        }

        private void FilterEvents(string search = "")
        {
            EventsVisible.Clear();

            if (search == "" || search == "Search")
                foreach (Event ev in Events)
                    EventsVisible.Add(ev);

            else
                foreach(Event ev in Events)
                    if (ev.Name.ToLower().Contains(search.ToLower()))
                        EventsVisible.Add(ev);
        }
    }
}