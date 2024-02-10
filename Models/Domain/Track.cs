namespace music_web_service1.Models.Domain
{
    public class Track
    {
        public string Name { get; set; }
        public string Audio { get; set; }

        public Track(string name, string audio)
        {
            Name = name;
            Audio = audio;
        }

        public Track() { }
    }
}
