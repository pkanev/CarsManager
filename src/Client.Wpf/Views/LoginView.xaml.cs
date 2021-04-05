using System.Windows;
using Client.Core.Models;
using Client.Core.ViewModels;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace Client.Wpf.Views
{
    public partial class LoginView : MvxWpfView
    {
        private IMvxInteraction<NotificationBox> _interaction;
        public IMvxInteraction<NotificationBox> Interaction
        {
            get => _interaction;
            set
            {
                if (_interaction != null)
                    _interaction.Requested -= OnInteractionRequested;

                if (value != null)
                {
                    _interaction = value;
                    _interaction.Requested += OnInteractionRequested;
                }
            }
        }

        public LoginView()
        {
            InitializeComponent();
            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(this).For(view => view.Interaction).To(viewModel => viewModel.NotificationInteraction).OneWay();
            set.Apply();
        }

        private void OnInteractionRequested(object sender, MvxValueEventArgs<NotificationBox> eventArgs)
        {
            var notification = eventArgs.Value;
            MessageBox.Show(notification.Message);
            notification.Callback();
        }
    }
}
