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

            FullName = model.Name.ToString();
        }
    }
}
