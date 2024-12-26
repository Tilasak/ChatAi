using Microsoft.Maui.Controls;

namespace ChatAi
{
    public partial class TranslatorPage : ContentPage
    {
        public TranslatorPage()
        {
            InitializeComponent();
        }

        private async void OnTranslateButtonClicked(object sender, EventArgs e)
        {
            string inputText = userInput.Text;
            if (!string.IsNullOrWhiteSpace(inputText))
            {
                // Заглушка для перевода
                string translatedText = "Это заглушка перевода.";
                resultLabel.Text = translatedText;
            }
        }
    }
}