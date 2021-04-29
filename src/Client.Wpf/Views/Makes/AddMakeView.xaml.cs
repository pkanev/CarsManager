using System.Windows.Input;
using Client.Wpf.Views.Common;

namespace Client.Wpf.Views.Makes
{
    public partial class AddMakeView : BaseWindow
    {
        public AddMakeView()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && CreateButton.IsEnabled)
                CreateButton.Command.Execute(CreateButton.CommandParameter);
        }
    }
}
