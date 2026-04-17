using System.Collections.ObjectModel;
using MedTracker.Models;

namespace MedTracker.ViewModels
{
    public class InventoryViewModel
    {
        public ObservableCollection<Medicine> Medicines { get; set; }

        public InventoryViewModel()
        {
            Medicines = new ObservableCollection<Medicine>
            {
                // Тестові дані
                new Medicine { Name = "Парацетамол", Category = "Жарознижувальні", Quantity = 10 },
                new Medicine { Name = "Ібупрофен", Category = "Знеболювальні", Quantity = 5 }
            };
        }
    }
}