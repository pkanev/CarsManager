using System.Windows.Input;
using Client.Wpf.Views.Common;

namespace Client.Wpf.Views.Makes
{
    public partial class DeleteMakeView : BaseWindow
    {
        public DeleteMakeView()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && DeleteButton.IsEnabled)
                DeleteButton.Command.Execute(DeleteButton.CommandParameter);
        }
    }
}
