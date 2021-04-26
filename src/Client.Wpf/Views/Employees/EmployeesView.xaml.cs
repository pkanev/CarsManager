using System.Windows;
using System.Windows.Input;
using Client.Core.Models;
using Client.Core.ViewModels.Employees;
using Client.Wpf.Views.Common;
using Microsoft.Win32;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace Client.Wpf.Views.Employees
{
    public partial class EmployeesView : BaseView
    {
        private IMvxInteraction<UploadInteractionHandler> uploadInteraction;

        public IMvxInteraction<UploadInteractionHandler> UploadInteraction
        {
            get => uploadInteraction;
            set
            {
                if (uploadInteraction != null)
                    uploadInteraction.Requested -= OnUploadInteractionRequested;

                if (value != null)
                {
                    uploadInteraction = value;
                    uploadInteraction.Requested += OnUploadInteractionRequested;
                }
            }
        }

        public EmployeesView()
        {
            EventManager.RegisterClassHandler(typeof(System.Windows.Controls.TextBox), GotKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(OnGotKeyboardFocus));
            InitializeComponent();

            var set = this.CreateBindingSet<EmployeesView, EmployeesViewModel>();
            set.Bind(this).For(view => view.UploadInteraction).To(viewModel => viewModel.UploadInteraction).OneWay();
            set.Apply();
        }

        private void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;

            if (textBox != null && !textBox.IsReadOnly && e.KeyboardDevice.IsKeyDown(Key.Tab))
                textBox.SelectAll();
        }

        private void OnUploadInteractionRequested(object sender, MvxValueEventArgs<UploadInteractionHandler> eventArgs)
        {
            var handler = eventArgs.Value;
            var dlg = new OpenFileDialog { Multiselect = false, Filter = "Image Files|*.jpg;*.jpeg;*.png;" };

            if (dlg.ShowDialog() == true)
                handler.Callback(dlg.FileName);
        }
    }
}
