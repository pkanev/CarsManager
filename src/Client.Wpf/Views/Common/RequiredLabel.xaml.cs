using System.ComponentModel;
using System.Windows.Controls;

namespace Client.Wpf.Views.Common
{
    public partial class RequiredLabel : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string label;

        public string Label
        {
            get => label;
            set
            {
                label = value;
                OnPropertyChanged(nameof(Label));
            }
        }

        public RequiredLabel()
        {
            InitializeComponent();
            DataContext = this;
        }

    }
}
