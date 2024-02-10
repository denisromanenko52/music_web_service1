namespace music_web_service1.Models.Domain
{
    public class SearchHistory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Audio { get; set; }
        public string Email { get; set; }

        public SearchHistory(string name, string audio, string email)
        {
            Name = name;
            Audio = audio;
            Email = email;
        }

        public SearchHistory() { }
    }
}
