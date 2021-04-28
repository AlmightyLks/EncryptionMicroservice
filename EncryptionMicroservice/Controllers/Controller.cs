using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using EncryptionMicroservice.Models;
using Newtonsoft.Json;

namespace EncryptionMicroservice.Controllers
{
    public class Controller
    {
        private List<EncryptedEntry> _encryptedEntries;

        public List<EncryptedEntry> EncryptedEntries { get => _encryptedEntries; set => _encryptedEntries = value; }

        public Controller()
        {
            EncryptedEntries = new List<EncryptedEntry>();
        }
        public Controller(List<EncryptedEntry> encryptedEntries)
        {
            EncryptedEntries = encryptedEntries;
        }

        public DecryptedEntry DecryptEntry(EncryptedEntry entry, string key)
        {
            return entry.Decrypt(key);
        }
        public EncryptedEntry EncryptEntry(DecryptedEntry entry, string key)
        {
            return entry.Encrypt(key);
        }
        public void FetchAllEntries()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var result = client.GetAsync("https://localhost:44316/api/Encryption").GetAwaiter().GetResult();
                    var jsonStr = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    EncryptedEntries = JsonConvert.DeserializeObject<List<EncryptedEntry>>(jsonStr);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}