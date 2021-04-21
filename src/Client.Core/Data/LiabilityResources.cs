using Client.Core.Models.Liabilities;

namespace Client.Core.Data
{
    public static class LiabilityResources
    {
        public static string GetLiabilityController(LiabilityType liabilityType) => liabilityType switch
        {
            LiabilityType.MOT => "mots",
            LiabilityType.CivilLiability => "civilliabilities",
            LiabilityType.CarInsurance => "carinsurances",
            LiabilityType.Vignette => "vignettes",
            _ => ""
        };

        public static string GetTranslatedLiability(LiabilityType liabilityType) => liabilityType switch
        {
            LiabilityType.MOT => "технически преглед",
            LiabilityType.CivilLiability => "застраховка \"ГО\"",
            LiabilityType.CarInsurance => "застраховка \"КАСКО\"",
            LiabilityType.Vignette => "винетка",
            _ => ""
        };
    }
}
