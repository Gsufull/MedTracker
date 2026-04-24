using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MedTracker.Models;

namespace MedTracker.Services
{
    // Сервіс для збереження ліків у JSON файл
    public static class MedicineService
    {
        private static readonly string DataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MedTracker");

        private static readonly string MedicinesFile = Path.Combine(DataFolder, "medicines.json");

        public static List<Medicine> LoadMedicines()
        {
            try
            {
                if (!File.Exists(MedicinesFile))
                    return GetDefaultMedicines();

                string json = File.ReadAllText(MedicinesFile);
                var list = JsonSerializer.Deserialize<List<Medicine>>(json);
                return list ?? GetDefaultMedicines();
            }
            catch
            {
                return GetDefaultMedicines();
            }
        }

        public static void SaveMedicines(List<Medicine> medicines)
        {
            Directory.CreateDirectory(DataFolder);
            string json = JsonSerializer.Serialize(medicines, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(MedicinesFile, json);
        }

        // Кілька прикладів ліків при першому запуску
        private static List<Medicine> GetDefaultMedicines()
        {
            return new List<Medicine>
            {
                new Medicine { Name = "Парацетамол", Category = "Жарознижувальні", Quantity = 10, ExpiryDate = new DateTime(2026, 12, 31) },
                new Medicine { Name = "Спазмалгон", Category = "Знеболювальні", Quantity = 5, ExpiryDate = new DateTime(2025, 6, 30) },
                new Medicine { Name = "Активоване вугілля", Category = "Шлунково-кишкові", Quantity = 20, ExpiryDate = new DateTime(2027, 1, 1) }
            };
        }
    }
}
