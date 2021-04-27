using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Wpf.Views.Account
{
    public partial class LoginView : EntryView
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void OnTextChanged(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = !string.IsNullOrWhiteSpace(PasswordBox.Password) && !string.IsNullOrWhiteSpace(UsernameBox.Text);
            LoginButton.CommandParameter = PasswordBox.Password;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && LoginButton.IsEnabled)
                LoginButton.Command.Execute(LoginButton.CommandParameter);
        }

    }
}
