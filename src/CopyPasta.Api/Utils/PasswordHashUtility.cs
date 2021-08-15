using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CopyPasta.Api.Utils
{
    public static class PasswordHashUtility
    {
        public static async Task<string> GenerateHashAsync(string password, string encryptionSalt)
        {
            using var sha256 = SHA256.Create();
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(password + encryptionSalt));
            var hash = await sha256.ComputeHashAsync(stream);
            return ByteArrayToHexViaByteManipulation(hash);
        }

        private static string ByteArrayToHexViaByteManipulation(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];
            byte b;
            for (int i = 0; i < bytes.Length; i++)
            {
                b = ((byte)(bytes[i] >> 4));
                c[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);
                b = ((byte)(bytes[i] & 0xF));
                c[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            }
            return new string(c);
        }
    }
}