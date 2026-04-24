using MedTracker.Models;

namespace MedTracker.Services
{
    // Зберігає поточного авторизованого користувача
    public static class SessionService
    {
        public static User CurrentUser { get; private set; }
        public static bool IsLoggedIn => CurrentUser != null;
        public static bool IsAdmin => CurrentUser?.IsAdmin == true;

        public static void SetUser(User user)
        {
            CurrentUser = user;
            Logger.Log($"Вхід: {user?.Username} (роль: {user?.Role})");
        }

        public static void Logout()
        {
            Logger.Log($"Вихід: {CurrentUser?.Username}");
            CurrentUser = null;
        }
    }
}
