using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EncryptionMicroservice.Models
{
    public abstract class DatabaseObject
    {
        private long _id;

        public long Id { get => _id; set => _id = value; }
    }
}