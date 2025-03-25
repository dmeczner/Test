using System;
using System.IO;

namespace Test.Common
{
    internal class Constants
    {
        public const string NEW_EVENT_TITLE = "New Event";
        public const string EDIT_EVENT_TITLE = "Edit Event";
        public static readonly string JSON_FILE_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "events.json");
    }
}
