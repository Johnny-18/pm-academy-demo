using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DepsWebApp.Tests
{
    public class RatesTests
    {
        private string _address;
        private HttpClient _client;

        public RatesTests(HttpClient client, string address)
        {
            _address = address;
            _client = client;
        }
        
        public async Task Rates_ConvertFrom_EUR_To_USD_MustBe_StatusCode_Ok()
        {
            var requestRoute = _address + "/EUR/USD";

            try
            {
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
            var requestRoute = _address + "/ASD/USD";
            try
            {
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