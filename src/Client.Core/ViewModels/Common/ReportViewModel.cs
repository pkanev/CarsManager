using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Core.Data;
using Client.Core.Dtos;
using Client.Core.Models;
using Client.Core.Rest;
using Client.Core.Utils;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Client.Core.ViewModels.Common
{
    public abstract class ReportViewModel<TItem> : SubPageViewModel
    {
        private ObservableCollection<TItem> items;
        private PaginatedListDto<TItem> pageData = new PaginatedListDto<TItem>();
        private IList<TItem> allItems;
        private MvxInteraction<ExportModel<TItem>> exportInteraction = new MvxInteraction<ExportModel<TItem>>();

        protected abstract IList<string> Properties { get; }

        public ObservableCollection<TItem> Items
        {
            get => items;
            set
            {
                SetProperty(ref items, value);
                RaisePropertyChanged(() => CanExport);
            }
        }

        public PaginatedListDto<TItem> PageData
        {
            get => pageData;
            set
            {
                SetProperty(ref pageData, value);
                Items = PageData.Items.ToObservableCollection();
            }
        }

        public IList<TItem> AllItems
        {
            get => allItems;
            set => SetProperty(ref allItems, value);
        }

        public bool CanExport => Items?.Count > 0;

        public IMvxInteraction<ExportModel<TItem>> ExportInteraction => exportInteraction;

        public IMvxCommand GoToNextPageCommand { get; set; }
        public IMvxCommand GoToPreviousPageCommand { get; set; }
        public IMvxAsyncCommand GetAllItemsCommand { get; set; }
        public IMvxAsyncCommand<ExportType> ExportCommand { get; set; }

        protected ReportViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            GoToNextPageCommand = new MvxAsyncCommand(GoToNextPage);
            GoToPreviousPageCommand = new MvxAsyncCommand(GoToPreviousPage);
            GetAllItemsCommand = new MvxAsyncCommand(GetAllItems);
            ExportCommand = new MvxAsyncCommand<ExportType>(Export);
        }

        public async override Task Initialize() => await GetItems();

        private async Task GoToNextPage()
        {
            if (!pageData.HasNextPage)
                return;

            await GetItems(pageData.PageIndex + 1);
        }

        private async Task GoToPreviousPage()
        {
            if (!pageData.HasPreviousPage)
                return;

            await GetItems(pageData.PageIndex - 1);
        }

        protected abstract Task GetItems(int pageNumber = 1);

        protected abstract Task GetAllItems();

        private async Task Export(ExportType exportType)
        {
            await GetAllItems();
            RaiseExport(exportType);
        }

        private void RaiseExport(ExportType exportType)
        {
            exportInteraction.Raise(
                new ExportModel<TItem>
                {
                    Items = AllItems,
                    Properties = Properties,
                    ExportType = exportType,
                });
        }
    }

    public abstract class ReportViewModel<TItem, TParam> : SubPageViewModel<TParam>
    {
        private ObservableCollection<TItem> items;
        private PaginatedListDto<TItem> pageData = new PaginatedListDto<TItem>();
        private IList<TItem> allItems;
        private MvxInteraction<ExportModel<TItem>> exportInteraction = new MvxInteraction<ExportModel<TItem>>();

        protected abstract IList<string> Properties { get; }

        public ObservableCollection<TItem> Items
        {
            get => items;
            set
            {
                SetProperty(ref items, value);
                RaisePropertyChanged(() => CanExport);
            }
        }

        public PaginatedListDto<TItem> PageData
        {
            get => pageData;
            set
            {
                SetProperty(ref pageData, value);
                Items = PageData.Items.ToObservableCollection();
            }
        }

        public IList<TItem> AllItems
        {
            get => allItems;
            set => SetProperty(ref allItems, value);
        }

        public bool CanExport => Items?.Count > 0;

        public IMvxInteraction<ExportModel<TItem>> ExportInteraction => exportInteraction;

        public IMvxCommand GoToNextPageCommand { get; set; }
        public IMvxCommand GoToPreviousPageCommand { get; set; }
        public IMvxAsyncCommand GetAllItemsCommand { get; set; }
        public IMvxAsyncCommand<ExportType> ExportCommand { get; set; }

        protected ReportViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            GoToNextPageCommand = new MvxAsyncCommand(GoToNextPage);
            GoToPreviousPageCommand = new MvxAsyncCommand(GoToPreviousPage);
            GetAllItemsCommand = new MvxAsyncCommand(GetAllItems);
            ExportCommand = new MvxAsyncCommand<ExportType>(Export);
        }

        public async override Task Initialize() => await GetItems();

        private async Task GoToNextPage()
        {
            if (!pageData.HasNextPage)
                return;

            await GetItems(pageData.PageIndex + 1);
        }

        private async Task GoToPreviousPage()
        {
            if (!pageData.HasPreviousPage)
                return;

            await GetItems(pageData.PageIndex - 1);
        }

        protected abstract Task GetItems(int pageNumber = 1);

        protected abstract Task GetAllItems();

        private async Task Export(ExportType exportType)
        {
            await GetAllItems();
            RaiseExport(exportType);
        }

        private void RaiseExport(ExportType exportType)
        {
            exportInteraction.Raise(
                new ExportModel<TItem>
                {
                    Items = AllItems,
                    Properties = Properties,
                    ExportType = exportType,
                });
        }
    }
}
