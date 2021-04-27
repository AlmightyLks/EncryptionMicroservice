using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EncryptionMicroservice.Models
{
    public sealed class EcryptedEntry
    {
        private int _id;
        private string _key;
        private byte[] _bytes;

        public int Id { get => _id; set => _id = value; }
        public string Key { get => _key; set => _key = value; }
        public byte[] Bytes { get => _bytes; set => _bytes = value; }
        public EcryptedEntry()
        {
            Id = 0;
            Key = "";
            Bytes = new byte[0];
        }
        public EcryptedEntry(int id, string key, byte[] bytes)
        {
            Id = id;
            Key = key;
            Bytes = bytes;
        }
        public EcryptedEntry(EcryptedEntry entry)
        {
            Id = entry.Id;
            Key = entry.Key;
            Bytes = entry.Bytes;
        }
    }
}