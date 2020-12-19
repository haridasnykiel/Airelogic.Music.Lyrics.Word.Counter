using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using Music.Lyrics.Word.Counter.Services;

namespace Music.Lyrics.Word.Counter
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var client = new HttpClient
            {
                BaseAddress = new Uri("https://musicbrainz.org/ws/2/")
            };
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            IMusicApiClient musicClient = new MusicApiClient(client);
 
            builder.Services.AddScoped(sp => musicClient);

            await builder.Build().RunAsync();
        }
    }
}
