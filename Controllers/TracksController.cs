using Microsoft.AspNetCore.Mvc;
using music_web_service1.Models.Domain;
using Newtonsoft.Json;

namespace music_web_service1.Controllers
{
    public class TracksController : Controller
    {
        private readonly string apiKey = "c2a5c4d5";
        private readonly HttpClient httpClient;

        public TracksController()
        {
            httpClient = new HttpClient();
        }

        public ActionResult SearchResult()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SearchTracks(string searchTerm)
        {
            string apiUrl = $"https://api.jamendo.com/v3.0/tracks/?client_id={apiKey}&format=jsonpretty&limit=25&namesearch={searchTerm}";

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

                var tracks = data.Results.Where(x => x.Audio != "").ToList();

                return View("Search", tracks);
            }

            return View();
        }
    }
}
