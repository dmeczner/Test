using Prism.Mvvm;
using System.ComponentModel;

namespace Test.Model
{
    public class Event : BindableBase, IDataErrorInfo
    {
        private int _id;
        private string _name;
        private string _place;
        private string _country;
        private uint _capacity;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Place
        {
            get => _place;
            set => SetProperty(ref _place, value);
        }

        public string Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        public uint Capacity
        {
            get => _capacity;
            set => SetProperty(ref _capacity, value);
        }

        public void ChangeValue(Event @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            Place = @event.Place;
            Country = @event.Country;
            Capacity = @event.Capacity;
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case nameof(Capacity):
                        if (Capacity <= 0)
                        {
                            result = "Capacity must be a positive number.";
                        }
                        break;
                }
                return result;
            }
        }

        public string Error => null;
    }
}
