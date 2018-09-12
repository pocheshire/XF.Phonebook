using System.Text;
using Phonebook.API.Models;

namespace Phonebook.Core.BL.ViewModels.Contacts.Items
{
    public class ContactItemVm
    {
        public ContactModel Model { get; }

        public string ImageSource { get; private set; }

        public string FullName { get; private set; }

        public ContactItemVm(ContactModel model)
        {
            Model = model;

            ImageSource = model.Image.Thumbnail;

            FullName = GetFullName(model.Name);
        }

        private string GetFullName(NameModel name)
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(name.Title))
            { 
                sb.Append(name.Title);
                sb.Append(" ");
            }
            if (!string.IsNullOrEmpty(name.First))
            {
                sb.Append(name.First); 
                sb.Append(" ");
            }
            if (!string.IsNullOrEmpty(name.Last))
            {
                sb.Append(name.Last); 
            }

            return sb.ToString();
        }
    }
}
