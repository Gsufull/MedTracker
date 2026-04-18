using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MedTracker.Models;

namespace MedTracker.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Medicine> Medicines { get; set; }

        private Medicine _selectedMedicine;
        public Medicine SelectedMedicine
        {
            get => _selectedMedicine;
            set
            {
                _selectedMedicine = value;
                OnPropertyChanged();
                // Якщо вибрали препарат зі списку, копіюємо його у форму для редагування
                if (_selectedMedicine != null)
                {
                    CurrentMedicine = new Medicine
                    {
                        Name = _selectedMedicine.Name,
                        Category = _selectedMedicine.Category,
                        Quantity = _selectedMedicine.Quantity
                    };
                }
            }
        }

        private Medicine _currentMedicine;
        public Medicine CurrentMedicine
        {
            get => _currentMedicine;
            set { _currentMedicine = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public InventoryViewModel()
        {
            Medicines = new ObservableCollection<Medicine>
            {
                new Medicine { Name = "Парацетамол", Category = "Жарознижувальні", Quantity = 10 },
                new Medicine { Name = "Спазмалгон", Category = "Знеболювальні", Quantity = 5 }
            };

            CurrentMedicine = new Medicine();

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            ClearCommand = new RelayCommand(Clear);
        }

        private bool CanSave(object obj)
        {
            return !string.IsNullOrWhiteSpace(CurrentMedicine?.Name) &&
                   !string.IsNullOrWhiteSpace(CurrentMedicine?.Category) &&
                   CurrentMedicine?.Quantity >= 0;
        }

        private void Save(object obj)
        {
            if (SelectedMedicine != null)
            {
                // ОНОВЛЕННЯ (Update)
                SelectedMedicine.Name = CurrentMedicine.Name;
                SelectedMedicine.Category = CurrentMedicine.Category;
                SelectedMedicine.Quantity = CurrentMedicine.Quantity;
            }
            else
            {
                // СТВОРЕННЯ (Create)
                Medicines.Add(new Medicine
                {
                    Name = CurrentMedicine.Name,
                    Category = CurrentMedicine.Category,
                    Quantity = CurrentMedicine.Quantity
                });
            }
            Clear(null);
        }

        private bool CanDelete(object obj) => SelectedMedicine != null;

        private void Delete(object obj)
        {
            // ВИДАЛЕННЯ (Delete)
            if (SelectedMedicine != null)
            {
                Medicines.Remove(SelectedMedicine);
                Clear(null);
            }
        }

        private void Clear(object obj)
        {
            SelectedMedicine = null;
            CurrentMedicine = new Medicine();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}