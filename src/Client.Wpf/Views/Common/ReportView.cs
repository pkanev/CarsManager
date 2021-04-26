using System.Windows;
using System.Windows.Input;
using Client.Core.Models;
using Client.Core.ViewModels.Common;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace Client.Wpf.Views.Common
{
    public class ReportView<TItem> : BaseView
    {
        private IMvxInteraction<ExportModel<TItem>> exportInteraction;
        private ReportViewModel<TItem> viewModel;

        public new ReportViewModel<TItem> ViewModel
        {
            get { return viewModel ?? (viewModel = base.ViewModel as ReportViewModel<TItem>); }
        }

        public IMvxInteraction<ExportModel<TItem>> ExportInteraction
        {
            get => exportInteraction;
            set
            {
                if (exportInteraction != null)
                    exportInteraction.Requested -= OnExportInteractionRequested;

                if (value != null)
                {
                    exportInteraction = value;
                    exportInteraction.Requested += OnExportInteractionRequested;
                }
            }
        }

        public ReportView()
        {
            Loaded += OnReportViewLoaded;
        }

        private void OnReportViewLoaded(object sender, RoutedEventArgs e)
        {
            var set = this.CreateBindingSet<ReportView<TItem>, ReportViewModel<TItem>>();
            set.Bind(this).For(view => view.ExportInteraction).To(viewModel => viewModel.ExportInteraction).OneWay();
            set.Apply();

            ViewModel?.SetupLoadingSpinner(
                showLoading: () => Dispatcher.Invoke(() => Mouse.OverrideCursor = Cursors.Wait),
                hideLoading: () => Dispatcher.Invoke(() => Mouse.OverrideCursor = null));

            Loaded -= OnReportViewLoaded;
        }

        protected virtual void OnExportInteractionRequested(object sender, MvxValueEventArgs<ExportModel<TItem>> eventArgs)
        {
            // no operation
        }
    }
}
