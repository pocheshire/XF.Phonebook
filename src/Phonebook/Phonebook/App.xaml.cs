using Phonebook.API.Services;
using Phonebook.API.Services.Implementations;
using Phonebook.Core.BL.Services;
using Phonebook.Core.BL.ViewModels.Contact;
using Phonebook.Core.BL.ViewModels.Contacts;
using Phonebook.Core.BL.ViewModels.PhotoViewer;
using Phonebook.Core.UI.Pages.Contact;
using Phonebook.Core.UI.Pages.Contacts;
using Phonebook.Core.UI.Pages.PhotoViewer;
using Phonebook.Core.UI.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Phonebook.Core
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            RegisterServices();

            RegisterViewModelMappings();

            SetMainPage();
        }

        private void RegisterServices()
        {
            DependencyService.Register<IContactsApiService, ContactsApiService>();
            DependencyService.Register<INavigationService, NavigationService>();
        }

        private void RegisterViewModelMappings()
        {
            var navigationService = DependencyService.Resolve<INavigationService>();

            navigationService.Register<ContactsListPage, ContactsListViewModel>();
            navigationService.Register<ContactInfoPage, ContactInfoViewModel>();
            navigationService.Register<PhotoViewerPage, PhotoViewerViewModel>();
        }

        private void SetMainPage()
        {
            var mainPage = new UI.Pages.Contacts.ContactsListPage();

            mainPage.SetViewModel(new ContactsListViewModel(DependencyService.Get<IContactsApiService>()));

            MainPage = new NavigationPage(mainPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
