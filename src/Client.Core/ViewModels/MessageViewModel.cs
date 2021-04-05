using System;
using System.Threading.Tasks;
using Client.Core.Models;
using Client.Core.Rest;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels
{
    class MessageViewModel : BaseViewModel<NavigationModel<MessageModel>>
    {
        private MessageModel message;
        private Func<Task> onConfirm;

        public MessageModel Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        public IMvxCommand ConfirmCommand { get; set; }
        public IMvxCommand CancelCommand { get; set; }

        public MessageViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            ConfirmCommand = new MvxAsyncCommand(ConfirmMessage);
            CancelCommand = new MvxAsyncCommand(() => NavigationService.Close(this));
        }

        public override void Prepare(NavigationModel<MessageModel> model)
        {
            Message = model.Data;
            onConfirm = model.Callback;
        }

        private async Task ConfirmMessage()
        {
            await onConfirm();
            await NavigationService.Close(this);
        }
    }
}
