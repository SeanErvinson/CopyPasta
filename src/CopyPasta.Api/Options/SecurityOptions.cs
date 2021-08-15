namespace CopyPasta.Api.Options
{
    public class SecurityOptions
    {
        public string Salt { get; set; } = string.Empty;
        public int LinkLength { get; set; } = 8;
    }
}