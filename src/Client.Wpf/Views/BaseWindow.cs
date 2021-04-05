using System.Windows;
using Client.Core.Models;
using Client.Core.ViewModels;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace Client.Wpf.Views
{
    public class BaseWindow : MvxWindow
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

        public BaseWindow()
        {
            var set = this.CreateBindingSet<BaseWindow, BaseViewModel>();
            set.Bind(this).For(view => view.DialogInteraction).To(viewModel => viewModel.NotificationInteraction).OneWay();
            set.Apply();
        }

        private void OnDialogInteractionRequested(object sender, MvxValueEventArgs<NotificationBox> eventArgs)
        {
            var notification = eventArgs.Value;
            MessageBox.Show(notification.Message, notification.Caption);
            if (notification.Callback != null)
                notification.Callback();
        }
    }
}

