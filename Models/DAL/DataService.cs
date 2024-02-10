using Microsoft.EntityFrameworkCore;
using music_web_service1.Models.Domain;
using System.Security.Cryptography;
using System.Text;

namespace music_web_service1.Models.DAL
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;

        public DataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistUser(string Email, string Password)
        {
            // Хэширование предоставленного пароля
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                string hashedPassword = builder.ToString();

                // Проверка наличия пользователя с зашифрованным паролем
                var exist = await _dbContext.User.AnyAsync(x => x.Email == Email && x.Password == hashedPassword);

                return exist;
            }
        }

        public async Task<bool> AddUser(string Email, string Password)
        {
            var exist = await _dbContext.User.AnyAsync(x => x.Email == Email);

            if (!exist)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Password));

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }

                    string hashedPassword = builder.ToString();

                    _dbContext.User.Add(new Account { Email = Email, Password = hashedPassword });

                    _dbContext.SaveChanges();

                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task AddSearchHistory(string Email, string Name, string Audio)
        {
            var exist = await _dbContext.SearchHistory.AnyAsync(x => x.Email == Email && x.Name == Name && x.Audio == Audio);

            if (!exist)
            {
                _dbContext.SearchHistory.Add(new SearchHistory { Name = Name, Audio = Audio, Email = Email });

                _dbContext.SaveChanges();
            }
        }

        public async Task<List<SearchHistory>> GetSearchHistory(string Email)
        {
            var exist = await _dbContext.SearchHistory.AnyAsync(x => x.Email == Email);

            List<SearchHistory> history = new List<SearchHistory>();

            if (exist)
            {
                var query = await _dbContext.SearchHistory.Where(x => x.Email == Email).ToListAsync();

                foreach (var item in query)
                {
                    history.Add(item);
                }
            }

            return history;
        }
    }
}
