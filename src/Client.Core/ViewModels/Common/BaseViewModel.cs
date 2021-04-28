using System;
using System.Threading.Tasks;
using Client.Core.Models;
using Client.Core.Rest;
using Client.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Client.Core.ViewModels.Common
{
    public abstract class BaseViewModel : MvxViewModel
    {
        private readonly IApiService apiService;
        private readonly IMvxNavigationService navigationService;
        private readonly ICurrentUserService currentUserService;
        private MvxInteraction<NotificationBox> notificationInteraction = new MvxInteraction<NotificationBox>();

        protected IMvxNavigationService NavigationService => navigationService;
        protected IApiService ApiService => apiService;
        protected ICurrentUserService CurrentUserService => currentUserService;

        public bool IsAdmin => currentUserService.CurrentUser.IsAdmin;

        public IMvxInteraction<NotificationBox> NotificationInteraction => notificationInteraction;

        protected BaseViewModel(IApiService apiService, IMvxNavigationService navigationService, ICurrentUserService currentUserService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
            this.currentUserService = currentUserService;
        }

        protected async Task ShowMessage(string message, string caption, Func<Task> callback, Func<Task> cancelCallback = null)
        {
            var messageModel = new MessageModel
            {
                Content = message,
                Caption = caption,
            };

            var navigation = new NavigationModel<MessageModel>
            {
                Data = messageModel,
                Callback = callback,
                CancelCallback = cancelCallback,
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

        public void SetupLoadingSpinner(Action showLoading, Action hideLoading)
        {
            apiService.OnCallStart = showLoading;
            apiService.OnCallEnd = hideLoading;
        }
    }

    public abstract class BaseViewModel<T> : MvxViewModel<T>
    {
        private readonly IApiService apiService;
        private readonly IMvxNavigationService navigationService;
        private readonly ICurrentUserService currentUserService;
        private MvxInteraction<NotificationBox> notificationInteraction = new MvxInteraction<NotificationBox>();

        protected IMvxNavigationService NavigationService => navigationService;
        protected IApiService ApiService => apiService;
        protected ICurrentUserService CurrentUserService => currentUserService;

        public bool IsAdmin => currentUserService.CurrentUser.IsAdmin;

        public IMvxInteraction<NotificationBox> NotificationInteraction => notificationInteraction;

        protected BaseViewModel(IApiService apiService, IMvxNavigationService navigationService, ICurrentUserService currentUserService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
            this.currentUserService = currentUserService;
        }

        protected async Task ShowMessage(string message, string caption, Func<Task> callback, Func<Task> cancelCallback = null)
        {
            var messageModel = new MessageModel
            {
                Content = message,
                Caption = caption,
            };

            var navigation = new NavigationModel<MessageModel>
            {
                Data = messageModel,
                Callback = callback,
                CancelCallback = cancelCallback,
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

        public void SetupLoadingSpinner(Action showLoading, Action hideLoading)
        {
            apiService.OnCallStart = showLoading;
            apiService.OnCallEnd = hideLoading;
        }
    }
}
