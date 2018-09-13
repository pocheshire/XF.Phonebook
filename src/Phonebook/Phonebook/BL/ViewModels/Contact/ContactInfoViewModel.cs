using System.Threading.Tasks;
using System.Windows.Input;
using Phonebook.API.Models;
using Phonebook.Core.BL.ViewModels.PhotoViewer;
using Plugin.Messaging;
using Xamarin.Forms;

namespace Phonebook.Core.BL.ViewModels.Contact
{
    public class ContactInfoViewModel : BaseViewModel
    {
        private ContactModel _bundle;

        private ICommand _openPhotoCommand;
        public ICommand OpenPhotoCommand => _openPhotoCommand ?? (_openPhotoCommand = MakeCommand(OnOpenPhotoExecute));

        private ICommand _callCommand;
        public ICommand CallCommand => _callCommand ?? (_callCommand = MakeCommand(OnCallExecute, () => !string.IsNullOrEmpty(Phone)));

        private ICommand _smsCommand;
        public ICommand SmsCommand => _smsCommand ?? (_smsCommand = MakeCommand(OnSmsExecute, () => !string.IsNullOrEmpty(Phone)));

        private ICommand _emailCommand;
        public ICommand EmailCommand => _emailCommand ?? (_emailCommand = MakeCommand(OnEmailExecute, () => !string.IsNullOrEmpty(Email)));

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value, nameof(ImageSource));
        }

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value, nameof(FullName));
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value, nameof(Phone));
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value, nameof(Email));
        }

        private void OnEmailExecute()
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                emailMessenger.SendEmail(Email);
            }
        }

        private void OnCallExecute()
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
            {
                phoneDialer.MakePhoneCall(Phone);
            }
        }

        private void OnSmsExecute()
        {
            var smsMessenger = CrossMessaging.Current.SmsMessenger;
            if (smsMessenger.CanSendSms)
            {
                smsMessenger.SendSms(Phone);
            }
        }

        private void OnOpenPhotoExecute()
        {
            NavigationService.Present(typeof(PhotoViewerViewModel), _bundle.Image);
        }

        public override void Prepare(object parameter)
        {
            _bundle = (ContactModel)parameter;
        }

        public override Task OnPageAppearing()
        {
            ImageSource = _bundle.Image.Medium;

            FullName = _bundle.Name.ToString();

            Phone = _bundle.Phone;

            Email = _bundle.Email;

            Device.BeginInvokeOnMainThread(() =>
            {
                ((Command)CallCommand).ChangeCanExecute();
                ((Command)EmailCommand).ChangeCanExecute();
                ((Command)SmsCommand).ChangeCanExecute();
            });

            return Task.CompletedTask;
        }
    }
}
