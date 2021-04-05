using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Client.Core.Models;
using Microsoft.Win32;
using MvvmCross.Base;
using MvvmCross.ViewModels;

namespace Client.Wpf.Views
{
    public class BaseVehicleView : BaseView
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

        public BaseVehicleView()
        {
            EventManager.RegisterClassHandler(typeof(System.Windows.Controls.TextBox), GotKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(OnGotKeyboardFocus));
        }

        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
