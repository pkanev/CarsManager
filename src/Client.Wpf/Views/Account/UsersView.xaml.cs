using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Client.Core.Models;
using Client.Core.Models.Authentication;
using Client.Core.ViewModels.Account;
using Client.Wpf.Utils;
using Client.Wpf.Views.Common;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace Client.Wpf.Views.Account
{
    public partial class UsersView : ReportView<UserModel>
    {
        private IMvxInteraction refreshInteraction;
        private UsersViewModel usersViewModel => ViewModel as UsersViewModel;

        public IMvxInteraction RefreshInteraction
        {
            get => refreshInteraction;
            set
            {
                if (refreshInteraction != null)
                    refreshInteraction.Requested -= OnRefreshInteractionRequested;

                if (value != null)
                {
                    refreshInteraction = value;
                    refreshInteraction.Requested += OnRefreshInteractionRequested;
                }
            }
        }

        public UsersView()
        {
            InitializeComponent();
            Loaded += UsersViewLoaded;
        }

        private void UsersViewLoaded(object sender, RoutedEventArgs e)
        {
            var set = this.CreateBindingSet<UsersView, UsersViewModel>();
            set.Bind(this).For(view => view.RefreshInteraction).To(viewModel => viewModel.RefreshInteraction).OneWay();
            set.Apply();

            Loaded -= UsersViewLoaded;
        }

        private void PrintPage(object sender, RoutedEventArgs e)
            => Printer.PrintWpfPreview(PageContent);

        protected override void OnExportInteractionRequested(object sender, MvxValueEventArgs<ExportModel<UserModel>> eventArgs)
        {
            var model = eventArgs.Value;
            var headers = ReportDataGrid.Columns
                .Where(c => c.Header != null)
                .Select(c => c.Header.ToString())
                .ToList();

            switch (model.ExportType)
            {
                case Core.Data.ExportType.Csv:
                    Exporter.ExportToCsv(headers, model.Items, model.Properties, ViewTitle.Text);
                    return;
                case Core.Data.ExportType.Excel:
                    Exporter.ExportToExcel(headers, model.Items, model.Properties, ViewTitle.Text);
                    return;
                default:
                    break;
            }
        }

        private void OnCheckBoxClicked(object sender, RoutedEventArgs e)
        {
            var checkbox = e.OriginalSource as CheckBox;
            if (checkbox == null)
                return;

            bool isChecked = checkbox.IsChecked.GetValueOrDefault();
            var user = checkbox.DataContext as UserModel;
            if (user?.IsAdmin == isChecked)
                return;

            usersViewModel.CheckAdminCommand.Execute(user);
        }

        private void OnAdminRightsChanged(object sender, RoutedEventArgs e)
        {
            var link = e.OriginalSource as Hyperlink;
            var user = link.DataContext as UserModel;
            if (user == null)
                return;

            usersViewModel.CheckAdminCommand.Execute(user);
        }

        private void OnRemoveUserClicked(object sender, RoutedEventArgs e)
        {
            var link = e.OriginalSource as Hyperlink;
            var user = link.DataContext as UserModel;
            if (user == null)
                return;

            usersViewModel.RemoveUserCommand.Execute(user);
        }

        private void OnRefreshInteractionRequested(object sender, EventArgs e)
        {
            ReportDataGrid.Items.Refresh();
        }
    }
}
