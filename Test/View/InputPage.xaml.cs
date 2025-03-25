using Test.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Test.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InputPage : Page
    {
        EventViewModel _eventViewModel;

        public InputPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _eventViewModel = e.Parameter as EventViewModel;
            DataContext = _eventViewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Save logic here
            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null)
            {
                rootFrame.Navigate(typeof(EventsPage));
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigation back logic
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
