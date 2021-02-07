using System;
using System.Net.Http;
using System.Threading.Tasks;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Menu;
using RequestProcessor.App.Services;

namespace RequestProcessor.App
{
    /// <summary>
    /// Entry point.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <returns>Returns exit code.</returns>
        private static async Task<int> Main()
        {
            Console.WriteLine("\nCustom Http client.");
            Console.WriteLine("Created by Zherybor Ivan.\n");
            
            try
            {
                var logger = new Logger();
                
                var options = new OptionsSource();

                var responseHandler = new ResponseHandler();
                var requestHandler = new RequestHandler(new HttpClient());
                
                var performer = new RequestPerformer(requestHandler, responseHandler, logger);
                
                var mainMenu = new MainMenu(performer, options, logger);
                return await mainMenu.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Critical unhandled exception");
                Console.WriteLine(ex);
                return -1;
            }
        }
    }
}
