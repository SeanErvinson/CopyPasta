using System;

namespace CopyPasta.Api.Dtos
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime? ExpiresIn { get; set; }
        public string? Password { get; set; }
    }
}