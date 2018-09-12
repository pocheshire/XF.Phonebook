using System;
using Phonebook.BL.ViewModels.Contacts;
using Xamarin.Forms;

namespace Phonebook.UI.Pages.Contacts
{
    public partial class ContactsListPage : BasePage
    {
        public ContactsListPage()
        {
            InitializeComponent();
        }

        void OnItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            (ViewModel as ContactsListViewModel).SelectionChangedCommand.Execute(sender);
        }
    }
}
