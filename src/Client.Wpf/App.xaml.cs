using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Platforms.Wpf.Views;
using Serilog;

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

        protected override void OnStartup(StartupEventArgs e)
        {
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CarsManager", "Logs", "log.txt");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path: fileName, rollingInterval: RollingInterval.Day)
                .MinimumLevel.Error()
                .CreateLogger();

            base.OnStartup(e);
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Fatal(e.Exception, "Unexpected error");
            MessageBox.Show($"Получи се неочаквана грешка!: {e.Exception.Message}", "Грешка!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
