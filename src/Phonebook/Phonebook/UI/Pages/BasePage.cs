using System;
using System.Threading.Tasks;
using Phonebook.Core.BL.ViewModels;
using Xamarin.Forms;

namespace Phonebook.Core.UI.Pages
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(async () =>
            {
                if (ViewModel != null)
                    await ViewModel.OnPageAppearing();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Task.Run(async () =>
            {
                if (ViewModel != null)
                    await ViewModel.OnPageDissapearing();
            });
        }
    }
}
