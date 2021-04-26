using System.Windows;
using System.Windows.Input;
using Client.Wpf.Views.Common;

namespace Client.Wpf.Views.Account
{
    public partial class RegisterView : BaseView
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void OnTextChanged(object sender, RoutedEventArgs e)
        {
            RegisterButton.IsEnabled = !string.IsNullOrWhiteSpace(Username.Text)
                && !string.IsNullOrWhiteSpace(FirstName.Text)
                && !string.IsNullOrWhiteSpace(LastName.Text)
                && !string.IsNullOrWhiteSpace(Password.Password)
                && Password.Password == PasswordRepeat.Password;

            RegisterButton.CommandParameter = Password.Password;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && RegisterButton.IsEnabled)
                RegisterButton.Command.Execute(RegisterButton.CommandParameter);
        }
    }
}
