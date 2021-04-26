using System.Windows;
using System.Windows.Input;
using Client.Core.Models;
using Client.Core.ViewModels.Common;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace Client.Wpf.Views.Common
{
    public class BaseWindow : MvxWindow
    {
        private BaseViewModel viewModel;
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

        public BaseViewModel BaseViewModel
        {
            get { return viewModel ?? (viewModel = ViewModel as BaseViewModel); }
        }

        public BaseWindow()
        {
            Loaded += OnBaseWindowLoaded;
        }

        private void OnBaseWindowLoaded(object sender, RoutedEventArgs e)
        {
            var set = this.CreateBindingSet<BaseWindow, BaseViewModel>();
            set.Bind(this).For(view => view.DialogInteraction).To(viewModel => viewModel.NotificationInteraction).OneWay();
            set.Apply();

            BaseViewModel?.SetupLoadingSpinner(
                showLoading: () => Dispatcher.Invoke(() => Mouse.OverrideCursor = Cursors.Wait),
                hideLoading: () => Dispatcher.Invoke(() => Mouse.OverrideCursor = null));

            Loaded -= OnBaseWindowLoaded;
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

