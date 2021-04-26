using System.Windows;
using System.Windows.Input;
using Client.Wpf.Views.Common;

namespace Client.Wpf.Views.Account
{
    public partial class PasswordChangeView : BaseView
    {
        public PasswordChangeView()
        {
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ChangePasswordButton.IsEnabled = 
                NewPassword.Password == NewPasswordRepeat.Password
                && !string.IsNullOrWhiteSpace(Password.Password)
                && !string.IsNullOrWhiteSpace(NewPassword.Password);

            ChangePasswordButton.CommandParameter = (Password.Password, NewPassword.Password);
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && ChangePasswordButton.IsEnabled)
                ChangePasswordButton.Command.Execute(ChangePasswordButton.CommandParameter);
        }
    }
}
