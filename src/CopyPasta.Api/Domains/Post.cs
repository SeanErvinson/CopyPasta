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
        public string Link { get; set; }
        [FirestoreProperty]
        public string Content { get; set; }
        [FirestoreProperty]
        public string? Password { get; set; }
        [FirestoreProperty]
        public DateTime? ExpiresIn { get; set; }

        private Post() { }
        public Post(string link, string content)
        {
            Id = Guid.NewGuid();
            Content = content;
            Link = link;
        }

        public async Task SetPassword(string password, string salt)
        {
            Password = await PasswordHashUtility.GenerateHashAsync(password, salt);
        }
    }
}