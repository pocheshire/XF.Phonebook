using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Phonebook.Core.BL.Services;
using Xamarin.Forms;

namespace Phonebook.Core.BL.ViewModels
{
    public class BaseViewModel : IDisposable, INotifyPropertyChanged
    {
        readonly ConcurrentDictionary<string, object> _properties = new ConcurrentDictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            set => SetProperty(ref _loading, value, nameof(Loading));
        }

        protected INavigationService NavigationService { get; }

        public BaseViewModel()
        {
            NavigationService = DependencyService.Resolve<INavigationService>();
        }

        ~BaseViewModel()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        protected ICommand MakeCommand(Action commandAction)
        {
            return new Command(commandAction);
        }

        protected ICommand MakeCommand(Action<object> commandAction)
        {
            return new Command(commandAction);
        }

        protected ICommand MakeCommand(Action commandAction, Func<bool> canExecute)
        {
            return new Command(commandAction, canExecute);
        }

        protected ICommand MakeCommand(Action<object> commandAction, Func<object, bool> canExecute)
        {
            return new Command(commandAction, canExecute);
        }

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, object value, string name = null)
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

        protected void RaisePropertyChanged(string name)
        {
            OnPropertyChanged(name);
        }

        public virtual void Prepare(object parameter)
        {

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
