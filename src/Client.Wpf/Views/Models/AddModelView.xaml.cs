using System.Windows.Input;
using Client.Wpf.Views.Common;

namespace Client.Wpf.Views.Models
{
    public partial class AddModelView : BaseWindow
    {
        public AddModelView()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && AddButton.IsEnabled)
                AddButton.Command.Execute(AddButton.CommandParameter);
        }
    }
}
