using System.Windows;
using System.Windows.Input;
using Client.Wpf.Views.Common;

namespace Client.Wpf.Views.Account
{
    public partial class ProfileView : BaseView
    {
        public ProfileView()
        {
            InitializeComponent();
            Loaded += ProfileViewLoaded;
        }

        private void ProfileViewLoaded(object sender, RoutedEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(System.Windows.Controls.TextBox), GotKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(OnGotKeyboardFocus));

            Loaded -= ProfileViewLoaded;
        }

        private void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;

            if (textBox != null && !textBox.IsReadOnly && e.KeyboardDevice.IsKeyDown(Key.Tab))
                textBox.SelectAll();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && SubmitButton.IsEnabled)
                SubmitButton.Command.Execute(SubmitButton.CommandParameter);
        }
    }
}
