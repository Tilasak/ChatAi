using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace ChatAi
{
    public partial class ChatPage : ContentPage
    {
        public ChatPage()
        {
            InitializeComponent(); 
            UpdateRemainingRequestsLabel();
        }

        private int remainingRequests = 5; 

        private async void OnSendClicked(object sender, EventArgs e)
        {
            var userMessage = UserInput.Text;

           
            if (string.IsNullOrWhiteSpace(userMessage))
            {
                await DisplayAlert("Ошибка", "Введите сообщение перед отправкой.", "OK");
                return;
            }

            
            if (remainingRequests <= 0)
            {
               
                var aiEndMessage = "У вас закончились бесплатные запросы.";
                await DisplayAIResponse(aiEndMessage);
                return;
            }

            
            var userLabel = new Label
            {
                Text = $"Вы: {userMessage}",
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.Black
            };

            userLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => CopyToClipboard(userMessage))
            });

            ChatHistory.Children.Add(userLabel);

            
            var aiResponse = await GetAIResponse(userMessage);

            
            await DisplayAIResponse(aiResponse);

          
            UserInput.Text = string.Empty;

            
            remainingRequests--;
            UpdateRemainingRequestsLabel();
        }

        
        private async Task DisplayAIResponse(string aiResponse)
        {
            var aiLabel = new Label
            {
                Text = $"ИИ: {aiResponse}",
                FontAttributes = FontAttributes.Italic,
                TextColor = Colors.Black
            };

            
            aiLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => CopyToClipboard(aiResponse))
            });

            ChatHistory.Children.Add(aiLabel);
        }


        private async void CopyToClipboard(string text)
        {
            await Clipboard.SetTextAsync(text);
            await DisplayAlert("Копирование", "Текст скопирован в буфер обмена.", "OK");
        }


        private void UpdateRemainingRequestsLabel()
        {
            if (RemainingRequestsLabel != null)
            {
                RemainingRequestsLabel.Text = $"Осталось бесплатных запросов: {remainingRequests}";
            }
        }

        private async Task<string> GetAIResponse(string message)
        {
            using HttpClient client = new HttpClient();

            var requestBody = new
            {
                prompt = message,
                max_tokens = 150
            };

            var json = JsonSerializer.Serialize(requestBody);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "sk-qrstefghuvwxabcdqrstefghuvwxabcdqrstefgh");

            try
            {
                
                var response = await Task.Run(() => client.PostAsync("https://api.openai.com/v1/engines/davinci/completions", httpContent));

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseData = JsonDocument.Parse(jsonResponse);

                    if (responseData.RootElement.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
                    {
                        var aiMessageElement = choices[0].GetProperty("text");
                        string aiMessage = aiMessageElement.GetString();

                        return aiMessage?.Trim() ?? "Ответ от ИИ отсутствует.";
                    }
                    else
                    {
                        return "Не удалось извлечь ответ от ИИ.";
                    }
                }
                else
                {
                  
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return $"Ошибка при вызове API: {response.StatusCode} - {response.ReasonPhrase}. Ответ сервера: {errorMessage}";
                }
            }
            catch (HttpRequestException e)
            {
                return $"Ошибка сети: {e.Message}";
            }
            catch (JsonException e)
            {
                return $"Ошибка обработки ответа: {e.Message}";
            }
        }

        private void OnRateResponseClicked(object sender, EventArgs e)
        {
            
            DisplayAlert("Оценка", "Как вы оцениваете ответ ИИ?", "Хорошо", "Плохо");
        }
    }
}