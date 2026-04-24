using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MedTracker.Models;
using MedTracker.Services;

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
                if (_selectedMedicine != null)
                {
                    CurrentMedicine = new Medicine
                    {
                        Name = _selectedMedicine.Name,
                        Category = _selectedMedicine.Category,
                        Quantity = _selectedMedicine.Quantity,
                        ExpiryDate = _selectedMedicine.ExpiryDate,
                        Notes = _selectedMedicine.Notes
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
            var loaded = MedicineService.LoadMedicines();
            Medicines = new ObservableCollection<Medicine>(loaded);

            CurrentMedicine = new Medicine { ExpiryDate = System.DateTime.Today.AddYears(1) };

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
                SelectedMedicine.Name = CurrentMedicine.Name;
                SelectedMedicine.Category = CurrentMedicine.Category;
                SelectedMedicine.Quantity = CurrentMedicine.Quantity;
                SelectedMedicine.ExpiryDate = CurrentMedicine.ExpiryDate;
                SelectedMedicine.Notes = CurrentMedicine.Notes;
            }
            else
            {
                Medicines.Add(new Medicine
                {
                    Name = CurrentMedicine.Name,
                    Category = CurrentMedicine.Category,
                    Quantity = CurrentMedicine.Quantity,
                    ExpiryDate = CurrentMedicine.ExpiryDate,
                    Notes = CurrentMedicine.Notes
                });
            }
            
            MedicineService.SaveMedicines(Medicines.ToList());
            Logger.Log($"Збережено медикамент: {CurrentMedicine.Name}");
            Clear(null);
        }

        private bool CanDelete(object obj) => SelectedMedicine != null;

        private void Delete(object obj)
        {
            if (SelectedMedicine != null)
            {
                Logger.Log($"Видалено медикамент: {SelectedMedicine.Name}");
                Medicines.Remove(SelectedMedicine);
                MedicineService.SaveMedicines(Medicines.ToList());
                Clear(null);
            }
        }

        private void Clear(object obj)
        {
            SelectedMedicine = null;
            CurrentMedicine = new Medicine { ExpiryDate = System.DateTime.Today.AddYears(1) };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}