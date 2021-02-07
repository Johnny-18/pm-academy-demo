using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    public class OptionsSource : IOptionsSource
    {
        // path to the file
        private readonly string _path = "options.json";

        async Task<IEnumerable<(IRequestOptions, IResponseOptions)>> IOptionsSource.GetOptionsAsync()
        {
            try
            {
                await using var fs = new FileStream(_path, FileMode.Open, FileAccess.Read);
                
                var requestOptions = await JsonSerializer.DeserializeAsync<IEnumerable<RequestOptions>>(fs);

                return requestOptions.Select(x => (x as IRequestOptions, x as IResponseOptions));;
            }
            catch (Exception)
            {
                return new List<(IRequestOptions, IResponseOptions)>();
            }
        }
    }
}