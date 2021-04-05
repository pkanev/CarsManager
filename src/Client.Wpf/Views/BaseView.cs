using System;
using System.Windows;
using Client.Core.Models;
using Client.Core.ViewModels;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace Client.Wpf.Views
{
    public class BaseView : MvxWpfView
    {
        private IMvxInteraction<NotificationBox> dialogInteraction;

        public IMvxInteraction<NotificationBox> DialogInteraction
        {
            get => dialogInteraction;
            set
            {
                if (dialogInteraction != null)
                    dialogInteraction.Requested -= OnDialogInteractionRequested;

                if (value != null)
                {
                    dialogInteraction = value;
                    dialogInteraction.Requested += OnDialogInteractionRequested;
                }
            }
        }

        public BaseView()
        {
            var set = this.CreateBindingSet<BaseView, BaseViewModel>();
            set.Bind(this).For(view => view.DialogInteraction).To(viewModel => viewModel.NotificationInteraction).OneWay();
            set.Apply();
        }

        private void OnDialogInteractionRequested(object sender, MvxValueEventArgs<NotificationBox> eventArgs)
        {
            var notification = eventArgs.Value;
            var button = notification.IsPrompt
                ? MessageBoxButton.YesNo
                : MessageBoxButton.OK;

            var dialog = MessageBox.Show(notification.Message, notification.Caption, button);
            if (DialogIsAffirmative(dialog) && notification.Callback != null)
                notification.Callback();
        }

        private static bool DialogIsAffirmative(MessageBoxResult message)
        {
            return message == MessageBoxResult.OK || message == MessageBoxResult.Yes;
        }

        private void ShowMessage(string message, string caption, Action callback)
        {
            MessageBox.Show(message, caption);
            if (callback != null)
                callback();
        }
    }
}

