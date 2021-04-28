using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EncryptionMicroservice.Models
{
    public sealed class EncryptedEntry : DatabaseObject
    {
        private byte[] _bytes;

        public byte[] Bytes { get => _bytes; set => _bytes = value; }
        public EncryptedEntry()
        {
            Id = 0;
            Bytes = new byte[0];
        }
        public EncryptedEntry(int id, byte[] bytes)
        {
            Id = id;
            Bytes = bytes;
        }
        public EncryptedEntry(EncryptedEntry entry)
        {
            Id = entry.Id;
            Bytes = entry.Bytes;
        }

        public DecryptedEntry Decrypt(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException();
            }
            DecryptedEntry result = new DecryptedEntry();


            byte[] encSchluessel = Encoding.UTF8.GetBytes(key);
            int k = 0;

            byte[] extendedKeyBytes = new byte[Bytes.Length];

            for (int i = 1; i <= Bytes.Length; i++)
            {
                if (k == key.Length)
                {
                    k = 0;
                    extendedKeyBytes[i - 1] = encSchluessel[k];
                    k++;
                }
                else
                {
                    extendedKeyBytes[i - 1] = encSchluessel[k];
                    k++;
                }
            }

            byte[] encryptedContentBytes = new byte[Bytes.Length];

            for (int i = 0; i < Bytes.Length; i++)
            {
                encryptedContentBytes[i] = (byte)(Bytes[i] + extendedKeyBytes[i]);
            }

            result.Id = Id;
            result.Content = Encoding.UTF8.GetString(encryptedContentBytes);
            return result;
        }

        public EncryptedEntry Post()
        {
            EncryptedEntry entry = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    StringContent jsonContent = new StringContent(
                        JsonConvert.SerializeObject(this),
                        Encoding.UTF8,
                        "application/json"
                        );
                    var result = client.PostAsync("https://localhost:44316/api/Encryption/", jsonContent)
                        .GetAwaiter()
                        .GetResult();
                    var jsonStr = result.Content.ReadAsStringAsync()
                        .GetAwaiter()
                        .GetResult();
                    entry = JsonConvert.DeserializeObject<EncryptedEntry>(jsonStr);
                }
            }
            catch
            {

            }
            return entry;
        }
        public void Put()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    StringContent jsonContent = new StringContent(
                        JsonConvert.SerializeObject(this),
                        Encoding.UTF8,
                        "application/json"
                        );
                    client.PutAsync($"https://localhost:44316/api/Encryption/{Id}", jsonContent)
                        .GetAwaiter()
                        .GetResult();
                }
            }
            catch
            {

            }
        }
        public void Delete()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DeleteAsync($"https://localhost:44316/api/Encryption/{Id}")
                        .GetAwaiter()
                        .GetResult();
                }
            }
            catch
            {

            }
        }
    }
}