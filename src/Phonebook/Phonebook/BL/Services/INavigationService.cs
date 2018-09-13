using System;
using Phonebook.Core.BL.ViewModels;
using Phonebook.Core.UI.Pages;

namespace Phonebook.Core.BL.Services
{
    public interface INavigationService
    {
        void Register<TView, TViewModel>()
            where TView : BasePage
            where TViewModel : BaseViewModel;

        void Push(Type viewModelType, object parameter);

        void Pop();

        void Present(Type viewModelType, object parameter);
    }
}
