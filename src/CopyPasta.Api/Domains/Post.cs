using System;
using System.Threading.Tasks;
using CopyPasta.Api.Firestore;
using CopyPasta.Api.Utils;
using Google.Cloud.Firestore;

namespace CopyPasta.Api.Domains
{
    [FirestoreData]
    public class Post
    {
        [FirestoreProperty(ConverterType = typeof(GuidConverter), Name = "Id")]
        public Guid Id { get; private set; }
        [FirestoreProperty]
        public string Link { get; private set; }
        [FirestoreProperty]
        public string Content { get; private set; }
        [FirestoreProperty]
        public string? Password { get; private set; }
        [FirestoreProperty]
        public DateTime? ExpiresIn { get; private set; }
        [FirestoreProperty]
        public DateTime CreatedOn { get; private set; }

        private Post() { }
        public Post(string link, string content)
        {
            Id = Guid.NewGuid();
            Content = content;
            Link = link;
            CreatedOn = DateTime.UtcNow;
        }

        public void AddExpiration(double? expirationMinutes)
        {
            ExpiresIn = DateTime.UtcNow.AddMinutes(expirationMinutes ?? 0);
        }

        public async Task SetPassword(string password, string salt)
        {
            Password = await PasswordHashUtility.GenerateHashAsync(password, salt);
        }
    }
}