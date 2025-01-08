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
        public ObservableCollection<Event> Events { get; set; } = [];

        public MainWindow()
        {
            InitializeComponent();

            Event event1 = new("Oasis Croke Park", new DateTime(2025, 06, 20), EventType.Music);
            Event event2 = new("Electric Picnic", new DateTime(2025, 08, 20), EventType.Music);
            Ticket ticket1 = new("Early Bird", 100m, 100);
            Ticket ticket2 = new("Platinum", 150m, 100);
            VipTicket vipTicket1 = new("Ticket and Hotel Package", 150m, 100m, "4* Hotel", 100);
            VipTicket vipTicket2 = new("Weekend Ticket", 200m, 100m, "With Camping", 100);

            event1.Tickets.Add(ticket1);
            event1.Tickets.Add(vipTicket1);
            event2.Tickets.Add(ticket2);
            event2.Tickets.Add(vipTicket2);

            Events.Add(event1);
            Events.Add(event2);

            LbxEvents.ItemsSource = Events;
        }

        private void LbxEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Event selectedEvent = (Event)LbxEvents.SelectedItem;
            LbxTickets.ItemsSource = selectedEvent.Tickets;
        }
    }
}