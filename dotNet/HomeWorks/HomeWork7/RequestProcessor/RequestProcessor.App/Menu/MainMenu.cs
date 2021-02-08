using System;
using System.Linq;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Services;

namespace RequestProcessor.App.Menu
{
    /// <summary>
    /// Main menu.
    /// </summary>
    internal class MainMenu : IMainMenu
    {
        private readonly IRequestPerformer _performer;
        private readonly IOptionsSource _options;
        private readonly ILogger _logger;
        
        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="options">Options source</param>
        /// <param name="performer">Request performer.</param>
        /// <param name="logger">Logger implementation.</param>
        public MainMenu(
            IRequestPerformer performer, 
            IOptionsSource options, 
            ILogger logger)
        {
            _performer = performer;
            _options = options;
            _logger = logger;
        }

        public async Task<int> StartAsync()
        {
            Console.WriteLine("Program is started.");
            Console.WriteLine("Reading from file...");
            _logger.Log("Start reading file.");
            
            // get options from file
            var requestOptions = await _options.GetOptionsAsync();
            
            _logger.Log("File was read.");
            Console.WriteLine("Data from file was read!");

            try
            {
                _logger.Log("Start sending messages.");
                Console.WriteLine("Sending requests to servers...");

                //sending requests to servers
                var tasks = requestOptions.Where(x => x.Item1.IsValid || x.Item2.IsValid)
                    .Select(x => _performer.PerformRequestAsync(x.Item1, x.Item2)).ToArray();

                // waiting all tasks
                await Task.WhenAll(tasks);
                
                _logger.Log("End sending messages.");
                Console.WriteLine("Requests was send and files was created!");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Data from file is missing or empty!");
                _logger.Log(e, e.Message);
                return -1;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Invalid parameters in file!");
                _logger.Log(e, e.Message);
                return -1;
            }
            catch (PerformException e)
            {
                Console.WriteLine("Something going wrong!");
                _logger.Log(e, e.Message);
                return -1;
            }
            
            Console.WriteLine("Program is ended!");
            Console.WriteLine("Goodbye!");

            return 0;
        }
    }
}
