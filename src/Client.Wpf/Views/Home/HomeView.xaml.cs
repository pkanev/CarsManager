using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using Client.Core.ViewModels.Home;
using Client.Wpf.Views.Common;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace Client.Wpf.Views.Home
{
    public partial class HomeView : BaseView
    {
        private HomeViewModel viewModel;

        private IMvxInteraction closeInteraction;

        public IMvxInteraction CloseInteraction
        {
            get => closeInteraction;
            set
            {
                if (closeInteraction != null)
                    closeInteraction.Requested -= OnCloseInteractionRequested;

                if (value != null)
                {
                    closeInteraction = value;
                    closeInteraction.Requested += OnCloseInteractionRequested;
                }
            }
        }

        public new HomeViewModel ViewModel
        {
            get { return viewModel ?? (viewModel = base.ViewModel as HomeViewModel); }
        }

        public HomeView()
        {
            InitializeComponent();
            Loaded += HomeViewViewLoaded;
        }

        private void HomeViewViewLoaded(object sender, RoutedEventArgs e)
        {
            var set = this.CreateBindingSet<HomeView, HomeViewModel>();
            set.Bind(this).For(view => view.CloseInteraction).To(viewModel => viewModel.CloseAppInteraction).OneWay();
            set.Apply();

            SetKeybindings();
            SetDateAndDay();
            Loaded -= HomeViewViewLoaded;
        }

        private void SetKeybindings()
        {
            var window = Window.GetWindow(this);
            if (window.InputBindings.Count > 0)
                return;

            window.InputBindings.Add(new KeyBinding(ViewModel.GoHomeCommand, new KeyGesture(Key.F1)));
            window.InputBindings.Add(new KeyBinding(ViewModel.GoToAddVehicleCommand, new KeyGesture(Key.F2)));
            window.InputBindings.Add(new KeyBinding(ViewModel.GoToMileageReportCommand, new KeyGesture(Key.F3)));
            window.InputBindings.Add(new KeyBinding(ViewModel.GoToAddRepairCommand, new KeyGesture(Key.F4)));
            window.InputBindings.Add(new KeyBinding(ViewModel.GoToEmployeesCommand, new KeyGesture(Key.F5)));
            window.InputBindings.Add(new KeyBinding(ViewModel.GoToCivilLiabilitiesCommand, new KeyGesture(Key.I, ModifierKeys.Control)));
            window.InputBindings.Add(new KeyBinding(ViewModel.GoToMotsCommand, new KeyGesture(Key.T, ModifierKeys.Control)));
            window.InputBindings.Add(new KeyBinding(ViewModel.GoToCarInsurancesCommand, new KeyGesture(Key.K, ModifierKeys.Control)));
            window.InputBindings.Add(new KeyBinding(ViewModel.GoToVignettesCommand, new KeyGesture(Key.J, ModifierKeys.Control)));
            window.InputBindings.Add(new KeyBinding(ViewModel.GoToVehiclesCommand, new KeyGesture(Key.F6)));
            window.InputBindings.Add(new KeyBinding(ViewModel.GoToRepairsCommand, new KeyGesture(Key.F7)));
            window.InputBindings.Add(new KeyBinding(ViewModel.GoToRoadBookCommand, new KeyGesture(Key.F8)));
        }

        private void SetDateAndDay()
        {
            var today = DateTime.Today;
            Date.Text = today.ToShortDateString();
            Day.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(today.DayOfWeek);
        }

        private void OnCloseInteractionRequested(object sender, EventArgs e)
            => Application.Current.Shutdown();
    }
}
