using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Client.Wpf.ViewModels
{
    public class MenuStripViewModel : MvxViewModel
    {
        public IMvxCommand OpenCommand { get; set; }

        public MenuStripViewModel()
        {
            OpenCommand = new MvxCommand(Open);
        }

        public void Open()
        {
            var a = 1 + 2;
        }
    }
}
