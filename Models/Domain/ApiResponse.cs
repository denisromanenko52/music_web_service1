namespace music_web_service1.Models.Domain
{
    public class ApiResponse
    {
        public List<Track> Results { get; set; }

        public ApiResponse(List<Track> results)
        {
            Results = results;
        }

        public ApiResponse() { }
    }
}
