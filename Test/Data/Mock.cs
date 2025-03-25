using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Test.Common;
using Test.Model;
using Test.View;

namespace Test.Data
{
    public static class Mock
    {
        private static List<Event> _events = new List<Event>
            {
                new Event { Id = 1, Name = "Event 1", Place = "Place 1", Country = "Country 1", Capacity = 100 },
                new Event { Id = 2, Name = "Event 2", Place = "Place 2", Country = "Country 2", Capacity = 200 },
                new Event { Id = 3, Name = "Event 3", Place = "Place 3", Country = "Country 3", Capacity = 300 }
            };

        public static Event GetEvent(int id)
        {
            return _events.Single(x => x.Id == id);
        }

        // TODO: remove it when implement the backend connection
        private static void SetNextId(Event @event)
        {
            int max = _events.Max(x => x.Id);
            @event.Id = ++max;
        }


        public static bool AddEvent(Event @event)
        {
            SetNextId(@event);
            _events.Add(@event);
            SaveToJson();
            return true;
        }

        public static bool UpdateEvent(Event @event)
        {
            var selectedEvent = _events.Single(x => x.Id == @event.Id);
            selectedEvent.ChangeValue(@event);
            SaveToJson();
            return true;
        }

        public static bool DeleteEvent(int id)
        {
            var eventToDelete = _events.Single(x => x.Id == id);
            _events.Remove(eventToDelete);
            SaveToJson();
            return true;
        }

        public static List<Event> LoadFromJson()
        {
            if (File.Exists(Constants.JSON_FILE_PATH))
            {
                var json = File.ReadAllText(Constants.JSON_FILE_PATH);
                _events = JsonConvert.DeserializeObject<List<Event>>(json) ?? new List<Event>();
            }
            return _events;
        }

        public static void SaveToJson()
        {
            var json = JsonConvert.SerializeObject(_events, Formatting.Indented);
            File.WriteAllText(Constants.JSON_FILE_PATH, json);
        }

        public static bool TryToAccess(User user)
        {
            string userName = "admin";
            string pass = "Admin12";

            return user.Login == userName && user.Password == pass;
        }
    }
}
