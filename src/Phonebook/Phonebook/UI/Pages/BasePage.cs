using System;
using Phonebook.BL.ViewModels;
using Xamarin.Forms;

namespace Phonebook.UI.Pages
{
	public class BasePage : ContentPage, IDisposable
    {
        protected BaseViewModel ViewModel { get; private set; }

        ~BasePage()
        {
            Dispose();
        }

        public void Dispose()
        {
            ViewModel?.Dispose();
        }

        public void SetViewModel(BaseViewModel viewModel)
        {
            BindingContext = ViewModel = viewModel;
        }
    }

    public class BasePage<T> : BasePage 
        where T : BaseViewModel
    {
        public new T ViewModel => ViewModel as T;
    }
}
