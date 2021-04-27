using System;
using System.Windows;
using Client.Core.ViewModels.Account;
using Client.Wpf.Views.Common;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace Client.Wpf.Views.Account
{
    public class EntryView : BaseView
    {
        private IMvxInteraction closeInteraction;

        public IMvxInteraction CloseInteraction
        {
            get => closeInteraction;
            set
            {
                if (closeInteraction != null)
                    closeInteraction.Requested -= OnCloseInteractionRequested;

                if (value != null)
                {
                    closeInteraction = value;
                    closeInteraction.Requested += OnCloseInteractionRequested;
                }
            }
        }

        public EntryView()
        {
            Loaded += EntryViewViewLoaded;
        }

        private void EntryViewViewLoaded(object sender, RoutedEventArgs e)
        {
            var set = this.CreateBindingSet<EntryView, EntryViewModel>();
            set.Bind(this).For(view => view.CloseInteraction).To(viewModel => viewModel.CloseAppInteraction).OneWay();
            set.Apply();

            Loaded -= EntryViewViewLoaded;
        }

        private void OnCloseInteractionRequested(object sender, EventArgs e)
            => Application.Current.Shutdown();
    }
}
