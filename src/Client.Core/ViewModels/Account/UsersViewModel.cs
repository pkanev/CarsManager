using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Core.Dtos;
using Client.Core.Models.Authentication;
using Client.Core.Rest;
using Client.Core.ViewModels.Common;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Client.Core.ViewModels.Account
{
    public class UsersViewModel : ReportViewModel<UserModel>
    {
        private IList<string> properties = new List<string>
        {
            nameof(UserModel.Username),
            nameof(UserModel.FirstName),
            nameof(UserModel.LastName),
            nameof(UserModel.IsAdmin),
        };

        private MvxInteraction refreshInteraction = new MvxInteraction();

        protected override IList<string> Properties => properties;

        public IMvxInteraction RefreshInteraction => refreshInteraction;

        public IMvxCommand<UserModel> CheckAdminCommand { get; private set; }
        public IMvxCommand<UserModel> RemoveUserCommand { get; private set; }

        public UsersViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            CheckAdminCommand = new MvxAsyncCommand<UserModel>(ChangeAdminStatus);
            RemoveUserCommand = new MvxAsyncCommand<UserModel>(RemoveUser);
        }

        protected override async Task GetItems(int pageNumber = 1)
        {
            var response = await ApiService.GetAsync<PaginatedListDto<UserModel>>($"account/pages?pagenumber={pageNumber}");

            if (response.IsSuccessStatusCode)
                PageData = response.Content;
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        protected override async Task GetAllItems()
        {
            var response = await ApiService.GetAsync<IList<UserModel>>("account");

            if (response.IsSuccessStatusCode)
                AllItems = response.Content;
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task ChangeAdminStatus(UserModel user)
        {
            if (user.IsAdmin)
            {
                string message = $"Сигурни ли сте, че желаете да премахнете административните права на {user.FirstName} {user.LastName}?";
                await ShowMessage(
                    message,
                    "Премахване на права",
                    async () => await DoChangeAdminStatus(user),
                    () => {
                        refreshInteraction.Raise();
                        return Task.CompletedTask;
                    });
            }
            else
                await DoChangeAdminStatus(user);
        }

        private async Task DoChangeAdminStatus(UserModel user)
        {
            var response = await ApiService.PostAsync<string>($"account/{user.Id}/adminstatus", new { UserId = user.Id, IsAdmin = !user.IsAdmin });

            if (response.IsSuccessStatusCode)
            {
                user.IsAdmin = !user.IsAdmin;
                await RaisePropertyChanged(() => Items);
                refreshInteraction.Raise();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task RemoveUser(UserModel user)
            => await ShowMessage($"Сигурни ли сте, че желаете да изтриете {user.FirstName} {user.LastName} от базата?", "Изтриване на потребител", async () =>
            {
                var response = await ApiService.DeleteAsync<string>($"account/{user.Id}");

                if (response.IsSuccessStatusCode)
                {
                    Items.Remove(user);
                    await RaisePropertyChanged(() => Items);
                    refreshInteraction.Raise();
                }
                else
                    RaiseNotification(response.Error, "Грешка!!!");
            });
    }
}
