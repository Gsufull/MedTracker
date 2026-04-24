using System;
using System.Collections.Generic;
using System.IO;
using MedTracker.Models;

namespace MedTracker.Services
{
    // Простий логер — пише рядки у файл логів
    public static class Logger
    {
        private static readonly string LogFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MedTracker", "app.log");

        public static void Log(string message)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogFile));
                string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
                File.AppendAllText(LogFile, line + Environment.NewLine);
            }
            catch
            {
                // Ігноруємо помилки логера
            }
        }

        public static List<string> ReadLogs()
        {
            try
            {
                if (!File.Exists(LogFile))
                    return new List<string>();
                return new List<string>(File.ReadAllLines(LogFile));
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}
