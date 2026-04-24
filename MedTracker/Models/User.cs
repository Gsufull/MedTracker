using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MedTracker.Models
{
    // Ролі користувача
    public enum UserRole
    {
        User,
        Admin
    }

    public class User : INotifyPropertyChanged
    {
        private string _username;
        private string _passwordHash;
        private UserRole _role;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        // Зберігаємо хеш пароля, а не сам пароль
        public string PasswordHash
        {
            get => _passwordHash;
            set { _passwordHash = value; OnPropertyChanged(); }
        }

        public UserRole Role
        {
            get => _role;
            set { _role = value; OnPropertyChanged(); }
        }

        public bool IsAdmin => Role == UserRole.Admin;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
