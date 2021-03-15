using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DepsWebApp.Tests
{
    public static class Program
    {
        private static string Path = "settings.json";
        
        public static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var json = await File.ReadAllTextAsync(Path);
            var settings = JsonSerializer.Deserialize<Settings>(json);
            if (settings == null)
            {
                Console.WriteLine("Something wrong with file!");
                return;
            }

            var ratesTests = new RatesTests(client, settings);
            var registerTests = new RegisterTests(client, settings.BaseAddress + "/" + settings.AuthApi);
            
            await registerTests.Register_WasCorrect_UserName_And_Password_Must_Be_StatusCodeOk();
            await registerTests.Register_WasIncorrect_UserName_And_Password_Length_LessThenSix_Must_Be_StatusCodeOk();
            await registerTests.Register_WasIncorrect_UserName_And_Password_Must_Be_StatusCodeOk();

            await ratesTests.Rates_ConvertFrom_EUR_To_USD_MustBe_StatusCode_Ok();
            await ratesTests.Rates_ConvertFrom_ASD_To_USD_MustBe_StatusCode_BadRequest();
            await ratesTests.Rates_ConvertFrom_ASD_To_USD_MustBe_StatusCode_Unauthorized();
        }
    }
}