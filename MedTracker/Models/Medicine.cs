using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MedTracker.Models
{
    public class Medicine : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _name;
        private string _category;
        private int _quantity;
        private DateTime _expiryDate;
        private string _notes;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        public DateTime ExpiryDate
        {
            get => _expiryDate;
            set { _expiryDate = value; OnPropertyChanged(); }
        }

        public string Notes
        {
            get => _notes;
            set { _notes = value; OnPropertyChanged(); }
        }

        // Перевіряємо чи термін не вийшов
        public bool IsExpired => ExpiryDate < DateTime.Today && ExpiryDate != DateTime.MinValue;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // --- ВАЛІДАЦІЯ ---
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Name) && string.IsNullOrWhiteSpace(Name))
                    return "Назва не може бути порожньою";
                if (columnName == nameof(Category) && string.IsNullOrWhiteSpace(Category))
                    return "Введіть категорію препарату";
                if (columnName == nameof(Quantity) && Quantity < 0)
                    return "Кількість не може бути від'ємною";
                return null;
            }
        }
    }
}