using System;
using System.Windows.Input;
using Phonebook.API.Models;

namespace Phonebook.Core.BL.ViewModels.PhotoViewer
{
    public class PhotoViewerViewModel : BaseViewModel
    {
        private ICommand _closeCommand;
        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = MakeCommand(NavigationService.Pop));

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value, nameof(ImageSource));
        }

        public override void Prepare(object parameter)
        {
            var imageModel = (ImageModel)parameter;

            ImageSource = imageModel.Large;
        }
    }
}
