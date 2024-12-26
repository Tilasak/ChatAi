using Microsoft.Maui.Controls;

namespace ChatAi
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
            // Здесь вы можете загрузить информацию о пользователе
            UserNameLabel.Text = "Имя пользователя"; // Заменить на реальное имя
            UserEmailLabel.Text = "user@example.com"; // Заменить на реальную почту
            SubscriptionStatusLabel.Text = "Активная"; // Заменить на реальный статус
            // Загрузите историю задач в ListView
        }

        private void OnNotificationSwitchToggled(object sender, ToggledEventArgs e)
        {
            // Логика для включения/выключения уведомлений
        }

        private void OnChangeThemeButtonClicked(object sender, EventArgs e)
        {
            // Логика для изменения цветовой темы
        }

        private void OnChangeLanguageButtonClicked(object sender, EventArgs e)
        {
            // Логика для изменения языка приложения
        }
    }
}
