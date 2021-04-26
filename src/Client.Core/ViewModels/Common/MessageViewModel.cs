using System;
using System.Threading.Tasks;
using Client.Core.Models;
using Client.Core.Rest;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Common
{
    class MessageViewModel : BaseViewModel<NavigationModel<MessageModel>>
    {
        private MessageModel message;
        private Func<Task> onConfirm;
        private Func<Task> onCancel;

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
            CancelCommand = new MvxAsyncCommand(Cancel);
        }

        public override void Prepare(NavigationModel<MessageModel> model)
        {
            Message = model.Data;
            onConfirm = model.Callback;
            onCancel = model.CancelCallback;
        }

        private async Task ConfirmMessage()
        {
            await onConfirm();
            await NavigationService.Close(this);
        }

        private async Task Cancel()
        {
            await onCancel?.Invoke();
            await NavigationService.Close(this);
        }
    }
}
