using System.Windows.Input;
using Client.Wpf.Views.Common;

namespace Client.Wpf.Views.Settings
{
    public partial class ServerSettingView : BaseWindow
    {
        public ServerSettingView()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && CloseButton.IsEnabled)
                CloseButton.Command.Execute(CloseButton.CommandParameter);
        }
    }
}
