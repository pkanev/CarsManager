using System.Windows.Input;
using Client.Wpf.Views.Common;

namespace Client.Wpf.Views.Models
{
    public partial class EditModelView : BaseWindow
    {
        public EditModelView()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && EditButton.IsEnabled)
                EditButton.Command.Execute(EditButton.CommandParameter);
        }
    }
}
