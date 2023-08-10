
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {

        // Replace this with your OpenAI API key
        var apiKey = "You key";


        // Define the prompt for the GPT-3 model
        var prompt = "Bana bir sarki yaz";

        // Define the completion parameters for the GPT-3 model
        var model = "text-davinci-002";
        var maxTokens = 1500;

        // Create an HTTP client instance
        using (var httpClient = new HttpClient())
        {
            // Set the base URL for the OpenAI API
            httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");

            // Add the API key to the HTTP headers
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            while (true)
            {
                Console.WriteLine("Yazini Yaz");
                var completionResponse = await httpClient.PostAsJsonAsync($"engines/{model}/completions", new
                {
                    prompt = Console.ReadLine(),
                    max_tokens = maxTokens
                });

                // If the response is successful, read the response content and display it
                if (completionResponse.IsSuccessStatusCode)
                {
                    var responseBody = await completionResponse.Content.ReadAsStringAsync();
                    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseBody);
                    //  Console.WriteLine(responseBody);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    StringBuilder str = new StringBuilder();
                    foreach (var item in myDeserializedClass.choices)
                    {
                        str.AppendLine(item.text);
                    }
                    Console.WriteLine(str);
                }
            }
            // Send a POST request to the OpenAI API with the prompt and completion parameters

        }
    }

    public class Choice
    {
        public string text { get; set; }
        public int index { get; set; }
        public object logprobs { get; set; }
        public string finish_reason { get; set; }
    }

    public class Root
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public List<Choice> choices { get; set; }
        public Usage usage { get; set; }
    }

    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }
}
