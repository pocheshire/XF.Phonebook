using System;
using System.Collections.Generic;
using Phonebook.Core.BL.Services;
using Phonebook.Core.BL.ViewModels;
using Phonebook.Core.UI.Pages;
using Xamarin.Forms;

namespace Phonebook.Core.UI.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Type> _viewToVmMappings;

        public NavigationService()
        {
            _viewToVmMappings = new Dictionary<Type, Type>();
        }

        private BasePage GetInitializedPage(Type viewModelType, object parameter)
        {
            var page = GetPage(viewModelType);
            var viewModel = GetViewModel(viewModelType);

            page.SetViewModel(viewModel);

            RunViewModelLifecycle(viewModel, parameter);

            return page;
        }

        private BasePage GetPage(Type viewModelType)
        {
            BasePage page;

            if (_viewToVmMappings.TryGetValue(viewModelType, out Type viewType))
            {
                var pageObject = Activator.CreateInstance(viewType);

                page = (BasePage)pageObject;
            }
            else
                throw new KeyNotFoundException($"Page for {viewModelType} not found");

            return page;
        }

        private BaseViewModel GetViewModel(Type viewModelType)
        {
            return (BaseViewModel)Activator.CreateInstance(viewModelType);
        }

        private void RunViewModelLifecycle(BaseViewModel viewModel, object parameter)
        {
            viewModel.Prepare(parameter);
        }

        private INavigation GetTopNavigation()
        {
            return (Application.Current.MainPage as NavigationPage)?.Navigation;
        }

        public void Register<TView, TViewModel>()
            where TView : BasePage
            where TViewModel : BaseViewModel
        {
            _viewToVmMappings[typeof(TViewModel)] = typeof(TView);
        }

        public void Pop()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var topNavigation = GetTopNavigation();

                if (topNavigation.ModalStack.Count > 0)
                    await topNavigation.PopModalAsync(true);
                else
                    await topNavigation.PopAsync(true);
            });
        }

        public void Present(Type viewModelType, object parameter)
        {
            var newPage = GetInitializedPage(viewModelType, parameter);

            Device.BeginInvokeOnMainThread(async () =>
            {
                await GetTopNavigation().PushModalAsync(newPage, true);
            });
        }

        public void Push(Type viewModelType, object parameter)
        {
            var newPage = GetInitializedPage(viewModelType, parameter);

            Device.BeginInvokeOnMainThread(async () =>
            {
                await GetTopNavigation().PushAsync(newPage, true);
            });
        }
    }
}
