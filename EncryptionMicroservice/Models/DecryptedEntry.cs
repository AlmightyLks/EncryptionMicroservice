using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EncryptionMicroservice.Models
{
    public sealed class DecryptedEntry : DatabaseObject
    {
        private string _content;

        public string Content { get => _content; set => _content = value; }

        public DecryptedEntry()
        {
            Id = 0;
            Content = "";
        }
        public DecryptedEntry(int id, string key, string content)
        {
            Id = id;
            Content = content;
        }
        public DecryptedEntry(DecryptedEntry entry)
        {
            Id = entry.Id;
            Content = entry.Content;
        }
        public EncryptedEntry Encrypt(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException();
            }
            EncryptedEntry result = new EncryptedEntry();

            byte[] decryptedEntry = Encoding.UTF8.GetBytes(Content);
            byte[] encryptedContent = new byte[decryptedEntry.Length];

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] extendedKeyBytes = new byte[decryptedEntry.Length];

            int k = 0;
            for (int j = 1; j <= decryptedEntry.Length; j++)
            {
                if (k == key.Length)
                {
                    k = 0;

                    extendedKeyBytes[(j - 1)] = keyBytes[k];

                    k++;
                }
                else
                {
                    extendedKeyBytes[(j - 1)] = keyBytes[k];
                    k++;
                }
            }

            for (int i = 0; i < decryptedEntry.Length; i++)
            {
                encryptedContent[i] = (byte)(decryptedEntry[i] - extendedKeyBytes[i]);
            }

            result.Id = Id;
            result.Bytes = encryptedContent;

            return result;
        }
    }
}