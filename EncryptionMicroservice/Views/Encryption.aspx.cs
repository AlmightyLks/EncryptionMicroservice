using EncryptionMicroservice.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EncryptionMicroservice.Models;
using System.Text;

namespace EncryptionMicroservice.Views
{
    public partial class Encryption : System.Web.UI.Page
    {
        private Controllers.Controller _verwalter;

        public Controllers.Controller Verwalter { get => _verwalter; set => _verwalter = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Verwalter == null)
            {
                Verwalter = Global.Verwalter;
            }
            Verwalter.FetchAllEntries();
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            DecryptedEntry entry = new DecryptedEntry();
            entry.Content = AddInputTextBox.Text;
            EncryptedEntry encrypedEntry = Verwalter.EncryptEntry(entry, AddKeyTextBox.Text);
            encrypedEntry = encrypedEntry.Post();
            if (encrypedEntry == null)
            {
                AddEntryResultLabel.Text = $"Something went wrong.";
            }
            else
            {
                AddEntryResultLabel.Text = $"Added entry id {encrypedEntry.Id}";
            }
        }

        protected void LookupButton_Click(object sender, EventArgs e)
        {
            LookupOutputTextBox.Text = "";
            if (!long.TryParse(LookupIdTextBox.Text, out long num))
            {
                return;
            }
            EncryptedEntry entry = Verwalter.EncryptedEntries.Find(_ => _.Id == num);
            if (entry == null)
            {
                return;
            }
            DecryptedEntry decrypted = entry.Decrypt(LookupKeyTextBox.Text);
            LookupOutputTextBox.Text = decrypted.Content;
        }
    }
}