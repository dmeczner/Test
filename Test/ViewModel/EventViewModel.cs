using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Test.Common;
using Test.Data;
using Test.Enums;
using Test.Model;

namespace Test.ViewModel
{
    public class EventViewModel : BaseViewModel
    {
        public ObservableCollection<Event> Events { get; set; } = [];

        public User Login { get; set; } = new User();

        public InputType TypeOfInput { get; set; }
        public string InputTitle { get; set; }

        private Event _provideToInput;
        public Event ProvideToInput
        {
            get => _provideToInput;
            set
            {
                SetField(ref _provideToInput, value);
            }
        }

        public RelayCommand NewOrEditEventCmd { get; set; }

        public RelayCommand DeleteEventCmd { get; set; }

        public RelayCommand LoginCmd { get; set; }

        public EventViewModel()
        {
            NewOrEditEventCmd = new RelayCommand(AddOrUpdateEvent, (obj) => true);
            DeleteEventCmd = new RelayCommand(DeleteEvent, (obj) => true);
            LoginCmd = new RelayCommand(LoginEvent, (obj) => true);
        }

        private void AddOrUpdateEvent(object obj)
        {
            try
            {
                if (TypeOfInput == InputType.NewEvent) { Mock.AddEvent(ProvideToInput); }
                else { Mock.UpdateEvent(ProvideToInput); }
            }
            catch (Exception ex)
            {
                if (TypeOfInput == InputType.NewEvent)
                {
                    Log.Error(ex, "Exception while try to add new event");
                }
                else
                {
                    Log.Error(ex, "Exception while try to update event");
                }
            }
        }

        private void DeleteEvent(object obj)
        {
            try
            {
                int id = (int)obj;
                if (Mock.DeleteEvent(id))
                {
                    Events.Remove(Events.Single(x => x.Id == id));
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception while try to delete");
            }
        }

        private void LoginEvent(object obj)
        {
            LoginHelper.IsUserLoggedIn = Mock.TryToAccess(Login);
        }

        public void SetInputType(InputType inputType, int tagToEdit = -1)
        {
            InputTitle = inputType == InputType.NewEvent ? Constants.NEW_EVENT_TITLE : Constants.EDIT_EVENT_TITLE;
            TypeOfInput = inputType;
            ProvideToInput = inputType == InputType.EditEvent ? Mock.GetEvent(tagToEdit) : new Event();
        }

        public async Task LoadEventsAsync(CancellationToken cancellationToken)
        {
            try
            {
                // TODO: do not forget to remove this line when you connected to the backend
                await Task.Delay(2000); // Simulate loading delay

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var events = Mock.LoadFromJson();

                foreach (var ev in events)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                    Events.Add(ev);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception while try to load all events");
            }
        }
    }
}
