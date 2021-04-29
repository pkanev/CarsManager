using System.Windows.Input;
using Client.Wpf.Views.Common;

namespace Client.Wpf.Views.Towns
{
    public partial class TownView : BaseWindow
    {
        public TownView()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && SaveButton.IsEnabled)
                SaveButton.Command.Execute(SaveButton.CommandParameter);
        }
    }
}
