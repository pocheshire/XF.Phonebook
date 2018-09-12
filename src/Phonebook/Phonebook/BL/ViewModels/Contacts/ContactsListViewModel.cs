using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Phonebook.BL.ViewModels.Contacts.Items;

namespace Phonebook.BL.ViewModels.Contacts
{
    public class ContactsListViewModel : BaseViewModel
    {
        private ICommand _selectionChangedCommand;
        public ICommand SelectionChangedCommand => _selectionChangedCommand ?? (_selectionChangedCommand = MakeCommand(OnSelectionChangedExecute));

        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = MakeCommand(OnSearchExecute));

        private ObservableCollection<ContactItemVm> _items;
        public ObservableCollection<ContactItemVm> Items
        {
            get => _items;
            set => SetProperty(ref _items, value, nameof(Items));
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value, nameof(SearchText));
        }

        public ContactsListViewModel()
        {
        }

        private void OnSelectionChangedExecute(object item)
        {
            //TODO: navigate to contact
        }

        private void OnSearchExecute()
        {
            //TODO: implement search
        }
    }
}
