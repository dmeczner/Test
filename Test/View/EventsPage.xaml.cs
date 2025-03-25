using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Test.Common;
using Test.Enums;
using Test.ViewModel;
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

namespace Test.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventsPage : Page
    {
        private EventViewModel _eventViewModel;
        private int _eventIdToDelete;
        private CancellationTokenSource _cancellationTokenSource;

        public EventsPage()
        {
            DataContext = _eventViewModel = new();
            this.InitializeComponent();
        }

        private async void LoadEvents()
        {
            using var _ = _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            LoadingDialog.ShowAsync();

            try
            {
                await _eventViewModel.LoadEventsAsync(cancellationToken);
            }
            finally
            {
                LoadingDialog.Hide();
            }
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            if (Window.Current.Content is Frame rootFrame)
            {
                _eventViewModel.SetInputType(InputType.NewEvent);
                rootFrame.Navigate(typeof(InputPage), _eventViewModel);
            }
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (Window.Current.Content is Frame rootFrame)
            {
                var button = (Button)sender;
                int tagToEdit = (int)button.Tag;
                _eventViewModel.SetInputType(InputType.EditEvent, tagToEdit);
                rootFrame.Navigate(typeof(InputPage), _eventViewModel);
            }
        }

        private async void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            _eventIdToDelete = (int)button.Tag;
            DeleteConfirmationDialog.Visibility = Visibility.Visible;
            await DeleteConfirmationDialog.ShowAsync();
        }



        private void DeleteConfirmationDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            _eventViewModel.DeleteEventCmd.Execute(_eventIdToDelete);
        }

        private void DeleteConfirmationDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // Do nothing, just close the dialog
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!LoginHelper.IsUserLoggedIn)
            {
                if (Window.Current.Content is Frame rootFrame)
                {
                    rootFrame.Navigate(typeof(LoginPage), _eventViewModel);
                }
            }
            else
            {
                LoadEvents();
            }
        }
    }


}
