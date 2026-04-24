using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MedTracker.Models;

namespace MedTracker.Services
{
    public static class ReminderService
    {
        private static readonly string DataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MedTracker");

        private static readonly string RemindersFile = Path.Combine(DataFolder, "reminders.json");

        public static List<Reminder> LoadReminders()
        {
            try
            {
                if (!File.Exists(RemindersFile))
                    return new List<Reminder>();

                string json = File.ReadAllText(RemindersFile);
                var list = JsonSerializer.Deserialize<List<Reminder>>(json);
                return list ?? new List<Reminder>();
            }
            catch
            {
                return new List<Reminder>();
            }
        }

        public static void SaveReminders(IEnumerable<Reminder> reminders)
        {
            Directory.CreateDirectory(DataFolder);
            string json = JsonSerializer.Serialize(reminders, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(RemindersFile, json);
        }
    }
}
