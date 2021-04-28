using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Core.Data;
using Client.Core.Dtos;
using Client.Core.Models.Issues;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.ViewModels.Common;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Issues
{
    public class IssuesViewModel : ReportViewModel<IssueModel, IssueType>
    {
        private IList<string> properties = new List<string>
        {
            nameof(IssueModel.LicencePlate),
            nameof(IssueModel.MakeAndModel),
            nameof(IssueModel.VehicleTypeName),
            nameof(IssueModel.Color),
            nameof(IssueModel.Mileage),
            nameof(IssueModel.MileageAtRepair),
            nameof(IssueModel.Remainder),
        };

        private IssueType issueType;

        private string action => issueType switch
        {
            IssueType.BeltMileage => "belts",
            IssueType.BrakeDisksMileage => "brakes",
            IssueType.BrakeLiningsMileage => "linings",
            IssueType.CoolantMileage => "coolant",
            IssueType.OilMileage => "oil",
            _ => throw new ArgumentException($"Invalid issue type: {issueType}"),
        };

        public string Caption => issueType switch
        {
            IssueType.BeltMileage => "Справка \"Смяна на ролки и ремъци\"",
            IssueType.BrakeDisksMileage => "Справка \"Смяна на спирачни дискове\"",
            IssueType.BrakeLiningsMileage => "Справка \"Смяна на накладки\"",
            IssueType.CoolantMileage => "Справка \"Смяна на антифриз\"",
            IssueType.OilMileage => "Справка \"Смяна на масло\"",
            _ => string.Empty,
        };

        protected override IList<string> Properties => properties;

        public IssuesViewModel(IApiService apiService, IMvxNavigationService navigationService, ICurrentUserService currentUserService)
            : base(apiService, navigationService, currentUserService)
        {
        }

        public override void Prepare(IssueType parameter)
        {
            issueType = parameter;
        }

        protected override async Task GetAllItems()
        {
            var response = await ApiService.GetAsync<IList<IssueModel>>($"issues/{action}");

            if (response.IsSuccessStatusCode)
            {
                var issues = response.Content;
                foreach (var issue in issues)
                    issue.VehicleTypeName = VehicleResources.GetVehicleType(issue.VehicleType);

                AllItems = issues;
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        protected override async Task GetItems(int pageNumber = 1)
        {
            var response = await ApiService.GetAsync<PaginatedListDto<IssueModel>>($"issues/{action}/pages?pagenumber={pageNumber}");

            if (response.IsSuccessStatusCode)
                PageData = response.Content;
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }
    }
}
