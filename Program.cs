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

            var musicBrainzClient = new HttpClient
            {
                BaseAddress = new Uri("https://musicbrainz.org/ws/2/"),
            };
            musicBrainzClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            IMusicInfoApiClient musicClient = new MusicInfoApiClient(musicBrainzClient);
            builder.Services.AddScoped(sp => musicClient);

            var lyricsOvhClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.lyrics.ovh/v1/"),
            };
            lyricsOvhClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ILyricsApiClient lyricsClient = new LyricsApiClient(lyricsOvhClient);
            builder.Services.AddScoped(sp => lyricsClient);

            ISongHandler songHandler = new SongHandler(musicClient, lyricsClient);
            builder.Services.AddScoped(sp => songHandler);

            await builder.Build().RunAsync();
        }
    }
}
