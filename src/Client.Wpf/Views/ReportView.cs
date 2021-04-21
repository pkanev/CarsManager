using Client.Core.Models;
using Client.Core.ViewModels;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace Client.Wpf.Views
{
    public class ReportView<TItem> : BaseView
    {
        private IMvxInteraction<ExportModel<TItem>> exportInteraction;

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
            var set = this.CreateBindingSet<ReportView<TItem>, ReportViewModel<TItem>>();
            set.Bind(this).For(view => view.ExportInteraction).To(viewModel => viewModel.ExportInteraction).OneWay();
            set.Apply();
        }

        protected virtual void OnExportInteractionRequested(object sender, MvxValueEventArgs<ExportModel<TItem>> eventArgs)
        {
            // no operation
        }
    }
}
