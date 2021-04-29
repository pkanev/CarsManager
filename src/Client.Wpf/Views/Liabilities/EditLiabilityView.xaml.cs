using System.Windows.Input;
using Client.Wpf.Views.Common;

namespace Client.Wpf.Views.Liabilities
{
    public partial class EditLiabilityView : BaseWindow
    {
        public EditLiabilityView()
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
