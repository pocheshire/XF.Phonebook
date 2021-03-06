﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Phonebook.API.Models;
using Phonebook.API.Services;
using Phonebook.Core.BL.ViewModels.Contact;
using Phonebook.Core.BL.ViewModels.Contacts.Items;

namespace Phonebook.Core.BL.ViewModels.Contacts
{
    public class ContactsListViewModel : BaseViewModel
    {
        private bool _dataLoaded;

        private IEnumerable<ContactModel> _bundle;

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

        public IContactsApiService ApiService { get; }

        public ContactsListViewModel(IContactsApiService apiService)
        {
            ApiService = apiService;
        }

        private void OnSelectionChangedExecute(object item)
        {
            NavigationService.Push(typeof(ContactInfoViewModel), ((ContactItemVm)item).Model);
        }

        private void OnSearchExecute()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Items = new ObservableCollection<ContactItemVm>(_bundle.Select(SetupItemVm));

                return;
            }
            
            var searchQuery = SearchText.ToLowerInvariant().Split(' ', ',', ';');
            Items = new ObservableCollection<ContactItemVm>(
                _bundle
                .Where(
                    x => searchQuery.Any(q => x.Email.ToLowerInvariant().Contains(q))
                        || searchQuery.Any(q => x.Name.Title.ToLowerInvariant().Contains(q))
                        || searchQuery.Any(q => x.Name.First.ToLowerInvariant().Contains(q))
                        || searchQuery.Any(q => x.Name.Last.ToLowerInvariant().Contains(q))
                        || searchQuery.Any(q => x.Phone.Contains(q))
                )
                .Select(SetupItemVm)
            );
        }

        public override async Task OnPageAppearing()
        {
            if (_dataLoaded)
                return;

            Loading = true;

            try
            {
                _bundle = await ApiService.GetContacts();

                Items = new ObservableCollection<ContactItemVm>(_bundle.Select(SetupItemVm));

                _dataLoaded = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                Loading = false;
            }
        }

        private ContactItemVm SetupItemVm(ContactModel model)
            => new ContactItemVm(model);
    }
}
