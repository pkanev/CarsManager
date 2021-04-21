using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Client.Core.Data;
using Client.Core.Dtos;
using Client.Core.Models;
using Client.Core.Models.Liabilities;
using Client.Core.Rest;
using Client.Core.Utils;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Client.Core.ViewModels
{
    public class AddVehicleViewModel : SubPageViewModel
    {
        private CreateVehicleDto vehicle;
        private VehicleType selectedVehicleType;
        private MakeModel make;
        private ObservableCollection<MakeModel> makes;
        private ModelModel model;
        private ObservableCollection<ModelModel> models;
        private MvxInteraction<UploadInteractionHandler> uploadInteraction = new MvxInteraction<UploadInteractionHandler>();

        private ValidityPeriod motPeriod = ValidityPeriods.MotValidityPeriods.First();
        private ValidityPeriod civilLiabilityPeriod = ValidityPeriods.CivilLiabilityValidityPeriods.First();
        private ValidityPeriod carInsurancePeriod = ValidityPeriods.CarInsurancePeriodValidityPeriods.First();
        private ValidityPeriod vignettePeriod = ValidityPeriods.VignetteValidityPeriods.First();

        private string color;
        private string imageFile;

        public VehicleType SelectedVehicleType
        {
            get => selectedVehicleType;
            set
            {
                SetProperty(ref selectedVehicleType, value);
                Task.Run(async()=> await GetModels());
            }
        }

        public int? Year
        {
            get => Vehicle.Year;
            set
            {
                Vehicle.Year = value;
                RaisePropertyChanged(() => Year);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public int EngineDisplacement
        {
            get => Vehicle.EngineDisplacement;
            set
            {
                Vehicle.EngineDisplacement = value;
                RaisePropertyChanged(() => EngineDisplacement);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public string LicencePlate
        {
            get => Vehicle.LicencePlate;
            set
            {
                Vehicle.LicencePlate = value;
                RaisePropertyChanged(() => LicencePlate);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public int Mileage
        {
            get => Vehicle.Mileage;
            set
            {
                Vehicle.Mileage = value;
                RaisePropertyChanged(() => Mileage);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public DateTime MOTStartDate
        {
            get => Vehicle.MOT.StartDate;
            set
            {
                Vehicle.MOT.StartDate = value;
                RaisePropertyChanged(() => MOTStartDate);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public ValidityPeriod MOTPeriod
        {
            get => motPeriod;
            set => SetProperty(ref motPeriod, value);
        }

        public DateTime CivilLiabilityStartDate
        {
            get => Vehicle.CivilLiability.StartDate;
            set
            {
                Vehicle.CivilLiability.StartDate = value;
                RaisePropertyChanged(() => CivilLiabilityStartDate);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public ValidityPeriod CivilLiabilityPeriod
        {
            get =>  civilLiabilityPeriod;
            set => SetProperty(ref civilLiabilityPeriod, value);
        }

        public DateTime CarInsuranceStartDate
        {
            get => Vehicle.CarInsurance.StartDate;
            set
            {
                Vehicle.CarInsurance.StartDate = value;
                RaisePropertyChanged(() => CarInsuranceStartDate);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public ValidityPeriod CarInsurancePeriod
        {
            get => carInsurancePeriod;
            set => SetProperty(ref carInsurancePeriod, value);
        }
        
        public ValidityPeriod VignettePeriod
        {
            get => vignettePeriod;
            set => SetProperty(ref vignettePeriod, value);
        }

        public DateTime FirstRegistration
        {
            get => Vehicle.FirstRegistration;
            set
            {
                Vehicle.FirstRegistration = value;
                RaisePropertyChanged(() => FirstRegistration);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public int OilMileage
        {
            get => Vehicle.OilMileage;
            set
            {
                Vehicle.OilMileage = value;
                RaisePropertyChanged(() => OilMileage);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public int BeltMileage
        {
            get => Vehicle.BeltMileage;
            set
            {
                Vehicle.BeltMileage = value;
                RaisePropertyChanged(() => BeltMileage);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public int BrakeLiningsMileage
        {
            get => Vehicle.BrakeLiningsMileage;
            set
            {
                Vehicle.BrakeLiningsMileage = value;
                RaisePropertyChanged(() => BrakeLiningsMileage);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public int BrakeDisksMileage
        {
            get => Vehicle.BrakeDisksMileage;
            set
            {
                Vehicle.BrakeDisksMileage = value;
                RaisePropertyChanged(() => BrakeDisksMileage);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public int CoolantMileage
        {
            get => Vehicle.CoolantMileage;
            set
            {
                Vehicle.CoolantMileage = value;
                RaisePropertyChanged(() => CoolantMileage);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public int FuelConsumption
        {
            get => Vehicle.FuelConsumption;
            set
            {
                Vehicle.FuelConsumption = value;
                RaisePropertyChanged(() => FuelConsumption);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public string Color
        {
            get => color;
            set => SetProperty(ref color, value);
        }

        public CreateVehicleDto Vehicle
        {
            get => vehicle;
            set
            {
                SetProperty(ref vehicle, value);
                RaisePropertyChanged(() => IsValid);
            }
        }

        public ObservableCollection<MakeModel> Makes
        {
            get => makes;
            set => SetProperty(ref makes, value);
        }

        public MakeModel Make
        {
            get => make;
            set
            {
                SetProperty(ref make, value);
                RaisePropertyChanged(() => CanEditMake);
                RaisePropertyChanged(() => CanDeleteMake);
                RaisePropertyChanged(() => CanAddModel);
                RaisePropertyChanged(() => CanRefreshModels);
                Task.Run(async()=> await GetModels());
            }
        }

        public ObservableCollection<ModelModel> Models
        {
            get => models;
            set
            {
                SetProperty(ref models, value);
                RaisePropertyChanged(() => CanSelectModel);
            }
        }

        public ModelModel Model
        {
            get => model;
            set
            {
                SetProperty(ref model, value);
                RaisePropertyChanged(() => CanEditModel);
                RaisePropertyChanged(() => CanDeleteModel);
                RaisePropertyChanged(() => IsValid);
            }
        }

        private FuelTypeModel fuelType = VehicleResources.FuelTypes[0];
        public FuelTypeModel FuelType
        {
            get => fuelType;
            set => SetProperty(ref fuelType, value);
        }

        private DateTime? vignetteStartDate;
        public DateTime? VignetteStartDate
        {
            get => vignetteStartDate;
            set
            {
                SetProperty(ref vignetteStartDate, value);
                RaisePropertyChanged(() => HasVignette);
            }
        }

        public string ImageFile
        {
            get => imageFile;
            set => SetProperty(ref imageFile, value);
        }

        public bool CanSelectModel => Models != null && Models.Count > 0;
        public bool CanEditMake => Make != null;
        public bool CanDeleteMake => Make != null;
        public bool CanAddModel => Make != null;
        public bool CanEditModel => Model != null;
        public bool CanDeleteModel => Model != null;
        public bool CanRefreshModels => Make != null;
        public bool HasVignette => VignetteStartDate != null;

        public bool IsValid => Model != null
            && Regex.IsMatch(Vehicle.LicencePlate.ToUpper(), @"^[ETYOPAHKXCBMВЕРТОАСХКНМ]{1,2}[0-9]{4}[ETYOPAHKXCBMВЕРТОАСХКНМ]{1,2}$")
            && Vehicle.MOT.StartDate != default
            && Vehicle.CivilLiability.StartDate != default
            && Vehicle.CarInsurance.StartDate != default;

        public List<ValidityPeriod> MOTValidityPeriods => ValidityPeriods.MotValidityPeriods;
        public List<ValidityPeriod> CivilLiabilityValidityPeriods => ValidityPeriods.CivilLiabilityValidityPeriods;
        public List<ValidityPeriod> CarInsurancePeriodValidityPeriods => ValidityPeriods.CarInsurancePeriodValidityPeriods;
        public List<ValidityPeriod> VignetteValidityPeriods => ValidityPeriods.VignetteValidityPeriods;
        public List<string> Colors => VehicleResources.Colors;
        public List<FuelTypeModel> FuelTypes => VehicleResources.FuelTypes;

        public IMvxCommand<VehicleType> ChooseVehicleTypeCommand { get; set; }
        public IMvxCommand SaveVehicleAndCloseCommand { get; set; }
        public IMvxCommand AddVehicleCommand { get; set; }
        public IMvxCommand DuplicateVehicleCommand { get; set; }
        public IMvxCommand ClearFormCommand { get; set; }
        public IMvxCommand SelectImageCommand { get; set; }
        public IMvxCommand RemoveImageCommand { get; set; }
        public IMvxCommand AddMakeCommand { get; set; }
        public IMvxCommand EditMakeCommand { get; set; }
        public IMvxCommand DeleteMakeCommand { get; set; }
        public IMvxCommand AddModelCommand { get; set; }
        public IMvxCommand EditModelCommand { get; set; }
        public IMvxCommand DeleteModelCommand { get; set; }

        public IMvxInteraction<UploadInteractionHandler> UploadInteraction => uploadInteraction;

        public AddVehicleViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            ChooseVehicleTypeCommand = new MvxCommand<VehicleType>(v => SelectedVehicleType = v);
            SaveVehicleAndCloseCommand = new MvxAsyncCommand(SaveVehicleAndClose);
            AddVehicleCommand = new MvxAsyncCommand(AddVehicle);
            DuplicateVehicleCommand = new MvxAsyncCommand(StoreVehicle);
            ClearFormCommand = new MvxCommand(InitializeForm);
            SelectImageCommand = new MvxCommand(SelectImage);
            RemoveImageCommand = new MvxCommand(() => ImageFile = null);
            AddMakeCommand = new MvxCommand(() => NavigationService.Navigate<AddMakeViewModel, NavigationModel>(new NavigationModel { Callback = GetMakes }));
            EditMakeCommand = new MvxCommand(() => NavigationService.Navigate<EditMakeViewModel, NavigationModel<MakeModel>>(new NavigationModel<MakeModel> { Data = Make, Callback = GetMakes }));
            DeleteMakeCommand = new MvxCommand(() => NavigationService.Navigate<DeleteMakeViewModel, NavigationModel<MakeModel>>( new NavigationModel<MakeModel> { Data = Make, Callback = GetMakes }));
            AddModelCommand = new MvxCommand(() => NavigationService.Navigate <AddModelViewModel, NavigationModel<MakeModel>> (new NavigationModel<MakeModel> { Data = Make, Callback = GetModels }));
            EditModelCommand = new MvxCommand(() => NavigationService.Navigate <EditModelViewModel, NavigationModel<ModelModel>>(new NavigationModel<ModelModel> { Data = Model, Callback = GetModels }));
            DeleteModelCommand = new MvxCommand(() => NavigationService.Navigate <DeleteModelViewModel, NavigationModel<ModelModel>>(new NavigationModel<ModelModel> { Data = Model, Callback = GetModels }));

            InitializeForm();
        }

        public override async Task Initialize()
        {
            await GetMakes();
            await base.Initialize();
        }

        private async Task GetMakes()
        {
            var response = await ApiService.GetAsync<GetMakesDto>("makes");
            if (response.IsSuccessStatusCode)
            {
                Makes = response.Content.Makes.ToObservableCollection();
                Make = Makes.FirstOrDefault();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetModels()
        {
            if (Make == null)
            {
                Models.Clear();
                return;
            }

            var response = await ApiService.GetAsync<GetModelsDto>($"models/make/{Make.Id}/type/{selectedVehicleType}");
            if (response.IsSuccessStatusCode)
            {
                Models = response.Content.Models.ToObservableCollection();
                Model = Models.FirstOrDefault();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private void InitializeForm()
        {
            ImageFile = string.Empty;
            Color = VehicleResources.Colors.FirstOrDefault();
            Vehicle = new CreateVehicleDto();
            Vehicle.MOT.EndDate = vehicle.MOT.StartDate.AddValidityPeriod(motPeriod);
            Vehicle.CivilLiability.EndDate = vehicle.MOT.StartDate.AddValidityPeriod(civilLiabilityPeriod);
            Vehicle.CarInsurance.EndDate = vehicle.MOT.StartDate.AddValidityPeriod(CarInsurancePeriod);
            RaisePropertyChanged(() => Vehicle);
        }

        private void SelectImage()
        {
            uploadInteraction.Raise(
                new UploadInteractionHandler
                {
                    Callback = (filename) => ImageFile = filename
                });
        }

        private async Task SaveVehicleAndClose()
        {
            if (await StoreVehicle() > 0)
                GoHome();
        }

        private async Task AddVehicle()
        {
            if (await StoreVehicle() > 0)
                InitializeForm();
        }

        private async Task<int> StoreVehicle()
        {
            if (!IsValid)
            {
                RaiseNotification("Моля въведете всички необходими данни", "Грешка!!!");
                return -1;
            }

            await StoreImage();

            vehicle.ModelId = Model.Id;
            vehicle.MOT.EndDate = vehicle.MOT.StartDate.AddValidityPeriod(motPeriod);
            vehicle.CivilLiability.EndDate = vehicle.CivilLiability.StartDate.AddValidityPeriod(civilLiabilityPeriod);
            vehicle.CarInsurance.EndDate = vehicle.CarInsurance.StartDate.AddValidityPeriod(carInsurancePeriod);

            if (VignetteStartDate != null)
                vehicle.Vignette = new LiabilityModel
                {
                    StartDate = VignetteStartDate.Value,
                    EndDate = VignetteStartDate.Value.AddValidityPeriod(vignettePeriod)
                };

            vehicle.Fuel = FuelType.Id;
            vehicle.Color = Color;
            var result = await ApiService.PostAsync<int>("vehicles", vehicle);

            if (result.IsSuccessStatusCode)
                return result.Content;

            if (!string.IsNullOrEmpty(vehicle.ImageName))
                await ApiService.DeleteFileAsync(vehicle.ImageName);

            RaiseNotification(result.Error, "Грешка!!!");
            return -1;
        }

        private async Task StoreImage()
        {
            if (string.IsNullOrEmpty(imageFile))
                return;

            var result = await ApiService.UploadAsync(imageFile);
            if (result.IsSuccessStatusCode)
                vehicle.ImageName = result.Content;
        }
    }
}
