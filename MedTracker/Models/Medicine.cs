using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MedTracker.Models
{
    public class Medicine : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _name;
        private string _category;
        private int _quantity;

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // --- ВАЛІДАЦІЯ ДАНИХ ---
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == nameof(Name) && string.IsNullOrWhiteSpace(Name))
                    result = "Назва не може бути порожньою";
                if (columnName == nameof(Category) && string.IsNullOrWhiteSpace(Category))
                    result = "Введіть категорію препарату";
                if (columnName == nameof(Quantity) && Quantity < 0)
                    result = "Кількість не може бути від'ємною";
                return result;
            }
        }
    }
}