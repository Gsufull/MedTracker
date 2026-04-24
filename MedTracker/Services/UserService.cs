using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MedTracker.Models;
using MedTracker.Helpers;

namespace MedTracker.Services
{
    // Сервіс для роботи з користувачами (збереження у JSON файл)
    public static class UserService
    {
        private static readonly string DataFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MedTracker");

        private static readonly string UsersFile = Path.Combine(DataFolder, "users.json");

        // Завантажуємо список користувачів з файлу
        public static List<User> LoadUsers()
        {
            try
            {
                if (!File.Exists(UsersFile))
                    return new List<User>();

                string json = File.ReadAllText(UsersFile);
                var users = JsonSerializer.Deserialize<List<User>>(json);
                return users ?? new List<User>();
            }
            catch
            {
                return new List<User>();
            }
        }

        // Зберігаємо список користувачів у файл
        public static void SaveUsers(List<User> users)
        {
            Directory.CreateDirectory(DataFolder);
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(UsersFile, json);
        }

        // Реєстрація нового користувача
        public static bool Register(string username, string password, UserRole role)
        {
            var users = LoadUsers();

            // Перевіряємо чи такий логін вже є
            if (users.Exists(u => u.Username.ToLower() == username.ToLower()))
                return false;

            users.Add(new User
            {
                Username = username,
                PasswordHash = PasswordHelper.Hash(password),
                Role = role
            });

            SaveUsers(users);
            return true;
        }

        // Вхід — перевіряємо логін і пароль
        public static User Login(string username, string password)
        {
            var users = LoadUsers();
            var user = users.Find(u => u.Username.ToLower() == username.ToLower());

            if (user != null && PasswordHelper.Verify(password, user.PasswordHash))
                return user;

            return null;
        }

        // Видалення користувача (для адміна)
        public static bool DeleteUser(string username)
        {
            var users = LoadUsers();
            int removed = users.RemoveAll(u => u.Username.ToLower() == username.ToLower());
            if (removed > 0)
            {
                SaveUsers(users);
                return true;
            }
            return false;
        }
    }
}
