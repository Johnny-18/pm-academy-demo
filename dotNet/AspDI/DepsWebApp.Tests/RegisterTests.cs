using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DepsWebApp.Tests
{
    public class RegisterTests
    {
        private string _address;
        private HttpClient _client;

        public RegisterTests(HttpClient client, string address)
        {
            _client = client;
            _address = address;
        }

        public async Task Register_WasCorrect_UserName_And_Password_Must_Be_StatusCodeOk()
        {
            var jsonBody = JsonSerializer.Serialize(new
            {
                Login = "string",
                Password = "string"
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            
            var url = _address + "/register";
            
            Console.WriteLine($"Sending request to {url} with body {jsonBody}");
            try
            {
                var response = await _client.PostAsync(url, content);
                
                var expected = "status code 200";
                var actual = $"status code {(int)response.StatusCode}";

                PrintResult(expected, actual);
            }
            catch(Exception)
            {
                Console.WriteLine("Something wrong with client!");
            }
        }
        
        public async Task Register_WasIncorrect_UserName_And_Password_Must_Be_StatusCodeOk()
        {
            var jsonBody = JsonSerializer.Serialize(new
            {
                Login = "",
                Password = ""
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            
            var url = _address + "/register";

            Console.WriteLine($"Sending request to {url} with body {jsonBody}");
            try
            {
                var response = await _client.PostAsync(url, content);

                var expected = "status code 400";
                var actual = $"status code {(int)response.StatusCode}";

                PrintResult(expected, actual);
            }
            catch (Exception)
            {
                Console.WriteLine("Something wrong with client!");
            }
        }
        
        public async Task Register_WasIncorrect_UserName_And_Password_Length_LessThenSix_Must_Be_StatusCodeOk()
        {
            var jsonBody = JsonSerializer.Serialize(new
            {
                Login = "qwe",
                Password = "qwe"
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            
            var url = _address + "/register";

            Console.WriteLine($"Sending request to {url} with body {jsonBody}");
            try
            {
                var response = await _client.PostAsync(url, content);

                var expected = "status code 400";
                var actual = $"status code {(int)response.StatusCode}";

                PrintResult(expected, actual);
            }
            catch (Exception)
            {
                Console.WriteLine("Something wrong with client!");
            }
        }
        
        private void PrintResult(string expected, string actual)
        {
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"This results equal: {actual == expected}.\n");
        }
    }
}