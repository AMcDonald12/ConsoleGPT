using Newtonsoft.Json;
using System.Text;

namespace ConsoleGPT_2;
class Program
{
    static string API_KEY = "Your API key here";
    static string endpoint = "https://api.openai.com/v1/completions";
    
    static async Task Main(string[] args)
    {
        //initialize client and add API Key to the header
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {API_KEY}");
        Console.WriteLine("Welcome to ConsoleGPT-2. Ask as many questions as you want\nType 'clear' to exit.");

        while (true)
        {
            //prompt user for question and construct request body
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Ask me anything: ");
            Console.ResetColor();

            string prompt = Console.ReadLine() ?? string.Empty;

            if (prompt == "clear")
            {
                break;
            }

            if (prompt.Length > 0)
            {
                RequestBody requestBody = new RequestBody(prompt);

                //serialize request body object and create http request content
                string jsonRequestBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                //send request to endpoint and convert to a string
                HttpResponseMessage responseMessage = await client.PostAsync(endpoint, content);
                string responseString = await responseMessage.Content.ReadAsStringAsync();

                //deserialize response to ResponseGPT object
                ResponseGPT? response = JsonConvert.DeserializeObject<ResponseGPT>(responseString);

                //Get answer text
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(response?.Choices?[0].Text);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Please input a question.");
                Console.ResetColor();
            }
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nGoodbye!");
    }
}

