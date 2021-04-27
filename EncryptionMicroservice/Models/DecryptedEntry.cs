using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncryptionMicroservice.Models
{
    public sealed class DecryptedEntry
    {
        private int _id;
        private string _key;
        private string _content;

        public int Id { get => _id; set => _id = value; }
        public string Key { get => _key; set => _key = value; }
        public string Content { get => _content; set => _content = value; }

        public DecryptedEntry()
        {
            Id = 0;
            Key = "";
            Content = "";
        }
        public DecryptedEntry(int id, string key, string content)
        {
            Id = id;
            Key = key;
            Content = content;
        }
        public DecryptedEntry(DecryptedEntry entry)
        {
            Id = entry.Id;
            Key = entry.Key;
            Content = entry.Content;
        }
    }
}