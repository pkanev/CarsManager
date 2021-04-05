using System.Windows;
using Client.Core.Models;
using MvvmCross.Base;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace Client.Wpf.Views
{
    public partial class InteractableView : MvxWpfView
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

        public InteractableView()
        {
            InitializeComponent();
        }

        private void OnInteractionRequested(object sender, MvxValueEventArgs<NotificationBox> eventArgs)
        {
            var notification = eventArgs.Value;
            MessageBox.Show(notification.Message);
            notification.Callback();
        }
    }
}
