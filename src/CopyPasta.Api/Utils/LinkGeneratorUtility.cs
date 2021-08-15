using System;
using System.Security.Cryptography;
using System.Text;

namespace CopyPasta.Api.Utils
{
    public static class LinkGeneratorUtility
    {
        internal static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string GetLink(int size)
        {
            byte[] data = new byte[4 * size];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetBytes(data);
            var result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }
    }
}