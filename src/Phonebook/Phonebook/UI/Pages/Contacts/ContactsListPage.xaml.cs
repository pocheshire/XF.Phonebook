using Phonebook.Core.BL.ViewModels.Contacts;

namespace Phonebook.Core.UI.Pages.Contacts
{
    public partial class ContactsListPage : BasePage
    {
        public ContactsListPage()
        {
            InitializeComponent();
        }

        void OnItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            (ViewModel as ContactsListViewModel).SelectionChangedCommand.Execute(e.Item);
        }
    }
}
