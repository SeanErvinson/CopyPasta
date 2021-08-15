using System;
using System.Threading.Tasks;
using CopyPasta.Api.Utils;

namespace CopyPasta.Api.Domains
{
    public class Post
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Link { get; set; }
        public string Content { get; set; }
        public string? Password { get; set; }
        public DateTime? ExpiresIn { get; set; }

        public Post(string link, string content)
        {
            Content = content;
            Link = link;
        }

        public async Task SetPassword(string password, string salt)
        {
            Password = await PasswordHashUtility.GenerateHashAsync(password, salt);
        }
    }
}