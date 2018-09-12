using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Phonebook.Core.BL.ViewModels
{
    public class BaseViewModel : IDisposable, INotifyPropertyChanged
    {
        readonly ConcurrentDictionary<string, object> _properties = new ConcurrentDictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = MakeCommand(NavigateBack));

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            set => SetProperty(ref _loading, value, nameof(Loading));
        }

        public BaseViewModel()
        {
            
        }

        ~BaseViewModel()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        protected ICommand MakeNavigateToCommand(object toViewModel)
        {
            return MakeCommand(async () => await NavigateTo(toViewModel));
        }

        protected async Task NavigateTo(object toViewModel)
        {
            //TODO: implement navigaiton service
            //DependencyService.Resolve<INavigation>().PushAsync()
        }

        private void NavigateBack()
        {
            //TODO: implement navigaiton service
            //DependencyService.Resolve<INavigation>().PopAsync()
        }

        protected ICommand MakeCommand(Action commandAction)
        {
            return new Command(commandAction);
        }

        protected ICommand MakeCommand(Action<object> commandAction)
        {
            return new Command(commandAction);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, object value, [CallerMemberName] string name = null)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            var isExists = _properties.TryGetValue(name, out var getValue);
            if (isExists && Equals(value, getValue))
                return false;

            _properties.AddOrUpdate(name, value, (s, o) => value);

            storage = (T)value;

            OnPropertyChanged(name);

            return true;
        }

        public virtual Task OnPageAppearing()
        {
            return Task.FromResult(0);
        }

        public virtual Task OnPageDissapearing()
        {
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
