using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    public class OptionsSource : IOptionsSource
    {
        private string _path;
        
        public OptionsSource(string path)
        {
            _path = path;
        }
        
        async Task<IEnumerable<RequestOptions>> IOptionsSource.GetOptionsAsync()
        {
            var jsonStrings = await File.ReadAllLinesAsync(_path);

            return JsonSerializer.Deserialize<IEnumerable<RequestOptions>>(jsonStrings);
        }
    }
}