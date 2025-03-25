using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AllPlace : Page
    {
        // Minta esemény osztály
        public class EventItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Place { get; set; }
            public int Capacity { get; set; }
        }
        public AllPlace()
        {
            this.InitializeComponent();
        }

        // Események betöltése
        private void LoadEvents()
        {
            var events = new ObservableCollection<EventItem>
    {
        new EventItem { Id=1, Name="Bebury Park", Place="Alexandria, Egypt", Capacity=7720 },
        // További elemek...
    };

            EventsGridView.ItemsSource = events;
        }

        // Gomb eseménykezelők
        private void CreateNewEvent_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(CreateEventPage));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var eventId = (int)button.Tag;
            //Frame.Navigate(typeof(EditEventPage), eventId);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var eventId = (int)button.Tag;
            // Törlés logika
        }
    }
}
