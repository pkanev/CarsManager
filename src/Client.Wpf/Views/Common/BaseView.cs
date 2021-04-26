using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Client.Core.Models;
using Client.Core.ViewModels.Common;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace Client.Wpf.Views.Common
{
    public class BaseView : MvxWpfView
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

        public BaseView()
        {
            Loaded += BaseViewLoaded;
        }

        private void BaseViewLoaded(object sender, RoutedEventArgs e)
        {
            var set = this.CreateBindingSet<BaseView, BaseViewModel>();
            set.Bind(this).For(view => view.DialogInteraction).To(viewModel => viewModel.NotificationInteraction).OneWay();
            set.Apply();

            BaseViewModel?.SetupLoadingSpinner(
                showLoading: () => Dispatcher.Invoke(() => Mouse.OverrideCursor = Cursors.Wait),
                hideLoading: () => Dispatcher.Invoke(() => Mouse.OverrideCursor = null));

            Loaded -= BaseViewLoaded;
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

        public void PriceValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var textbox = (TextBox)sender;
            var text = textbox.Text;
            text = text.Remove(textbox.SelectionStart, textbox.SelectionLength);
            text = text.Insert(textbox.SelectionStart, e.Text);
            Regex regex = new Regex("^[0-9]*[,]{0,1}[0-9]{0,2}$");
            e.Handled = !regex.IsMatch(text);
        }
    }
}

