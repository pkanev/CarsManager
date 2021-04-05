using System;
using System.Threading.Tasks;
using Client.Core.Models;
using Client.Core.Rest;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Client.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        private readonly IApiService apiService;
        private readonly IMvxNavigationService navigationService;
        private MvxInteraction<NotificationBox> notificationInteraction = new MvxInteraction<NotificationBox>();

        protected IMvxNavigationService NavigationService => navigationService;
        protected IApiService ApiService => apiService;

        public IMvxInteraction<NotificationBox> NotificationInteraction => notificationInteraction;

        protected BaseViewModel(IApiService apiService, IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
        }

        protected async Task ShowMessage(string message, string caption, Func<Task> callback)
        {
            var messageModel = new MessageModel
            {
                Content = message,
                Caption = caption,
            };

            var navigation = new NavigationModel<MessageModel>
            {
                Data = messageModel,
                Callback = callback
            };

            await NavigationService.Navigate<MessageViewModel, NavigationModel<MessageModel>>(navigation);
        }

        protected void RaiseNotification(string message, string caption, Action callback = null)
        {
            notificationInteraction.Raise(
                new NotificationBox
                {
                    Message = message,
                    Caption = caption,
                    Callback = callback,
                });
        }

        protected void RaisePrompt(string message, string caption, Action callback = null)
        {
            notificationInteraction.Raise(
                new NotificationBox
                {
                    Message = message,
                    Caption = caption,
                    Callback = callback,
                    IsPrompt = true,
                });
        }
    }

    public abstract class BaseViewModel<T> : MvxViewModel<T>
    {
        private readonly IApiService apiService;
        private readonly IMvxNavigationService navigationService;
        private MvxInteraction<NotificationBox> notificationInteraction = new MvxInteraction<NotificationBox>();

        protected IMvxNavigationService NavigationService => navigationService;
        protected IApiService ApiService => apiService;

        public IMvxInteraction<NotificationBox> NotificationInteraction => notificationInteraction;

        protected BaseViewModel(IApiService apiService, IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
        }

        protected async Task ShowMessage(string message, string caption, Func<Task> callback)
        {
            var messageModel = new MessageModel
            {
                Content = message,
                Caption = caption,
            };

            var navigation = new NavigationModel<MessageModel>
            {
                Data = messageModel,
                Callback = callback
            };

            await NavigationService.Navigate<MessageViewModel, NavigationModel<MessageModel>>(navigation);
        }

        protected void RaiseNotification(string message, string caption, Action callback = null)
        {
            notificationInteraction.Raise(
                new NotificationBox
                {
                    Message = message,
                    Caption = caption,
                    Callback = callback,
                });
        }

        protected void RaisePrompt(string message, string caption, Action callback = null)
        {
            notificationInteraction.Raise(
                new NotificationBox
                {
                    Message = message,
                    Caption = caption,
                    Callback = callback,
                    IsPrompt = true,
                });
        }
    }
}
