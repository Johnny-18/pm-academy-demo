using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DepsWebApp.Tests
{
    public class RatesTests
    {
        private readonly Settings _settings;
        private readonly HttpClient _client;

        public RatesTests(HttpClient client, Settings settings)
        {
            _settings = settings;
            _client = client;
        }
        
        public async Task Rates_ConvertFrom_EUR_To_USD_MustBe_StatusCode_Ok()
        {
            var requestRoute = GetRatesApi() + "/EUR/USD";
            try
            {
                var login = "string";
                var password = "string";
                await RegisterUserForRequest(login, password);
                
                var userBytes = Encoding.ASCII.GetBytes($"{login}:{password}");
                _client.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue( "BasicAuthentication",Convert.ToBase64String(userBytes));
                
                var response = await GetResponse(requestRoute);

                var expected = "status code 200";
                var actual = $"status code {(int)response.StatusCode}";
            
                PrintResult(expected, actual);
            }
            catch (Exception)
            {
                Console.WriteLine("Something wrong with client!");
            }
        }

        public async Task Rates_ConvertFrom_ASD_To_USD_MustBe_StatusCode_BadRequest()
        {
            var requestRoute = GetRatesApi() + "/ASD/USD";
            try
            {
                var login = "string";
                var password = "string";
                await RegisterUserForRequest(login, password);
                
                var userBytes = Encoding.ASCII.GetBytes($"{login}:{password}");
                _client.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue( "BasicAuthentication",Convert.ToBase64String(userBytes));
                
                var response = await GetResponse(requestRoute);

                var expected = "status code 400";
                var actual = $"status code {(int)response.StatusCode}";
            
                PrintResult(expected, actual);
            }
            catch (Exception)
            {
                Console.WriteLine("Something wrong with client!");
            }
        }
        
        public async Task Rates_ConvertFrom_ASD_To_USD_MustBe_StatusCode_Unauthorized()
        {
            var requestRoute = GetRatesApi() + "/ASD/USD";
            try
            {
                var login = "qwe";
                var password = "qwe";
                
                var userBytes = Encoding.ASCII.GetBytes($"{login}:{password}");
                _client.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue( "BasicAuthentication",Convert.ToBase64String(userBytes));
                
                var response = await GetResponse(requestRoute);

                var expected = "status code 401";
                var actual = $"status code {(int)response.StatusCode}";
            
                PrintResult(expected, actual);
            }
            catch (Exception)
            {
                Console.WriteLine("Something wrong with client!");
            }
        }

        private string GetRatesApi()
        {
            return _settings.BaseAddress + "/" + _settings.RatesApi;
        }

        private async Task RegisterUserForRequest(string login, string password)
        {
            var jsonBody = JsonSerializer.Serialize(new
            {
                Login = login,
                Password = password
            });

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            
            var url = _settings.BaseAddress + "/" + _settings.AuthApi + "/register";
            try
            {
                await _client.PostAsync(url, content);
            }
            catch(Exception)
            {
                Console.WriteLine("Something wrong with client!");
            }
        }

        private async Task<HttpResponseMessage> GetResponse(string url)
        {
            Console.WriteLine($"Sending request to {url}.");

            return await _client.GetAsync(url);
        }

        private void PrintResult(string expected, string actual)
        {
            Console.WriteLine($"Actual: {actual}");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"This results equal: {actual == expected}.\n");
        }
    }
}