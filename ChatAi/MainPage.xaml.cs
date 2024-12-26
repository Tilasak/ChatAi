using Microsoft.Maui.Controls;

namespace ChatAi
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnChatButtonClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ChatPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        private async void OnTranslatorButtonClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new TranslatorPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
        private async void OnProfileButtonClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ProfilePage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
    }
}