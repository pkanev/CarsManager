using System.Globalization;
using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Platforms.Wpf.Views;

namespace Client.Wpf
{
    public partial class App : MvxApplication
    {
        protected override void RegisterSetup()
        {
            this.RegisterSetupType<MvxWpfSetup<Core.App>>();
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("bg");
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("bg");
        }
    }
}
