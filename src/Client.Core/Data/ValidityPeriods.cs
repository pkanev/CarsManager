using System.Collections.Generic;
using Client.Core.Models;

namespace Client.Core.Data
{
    public static class ValidityPeriods
    {
        public static List<ValidityPeriod> MotValidityPeriods = new List<ValidityPeriod>
        {
            new ValidityPeriod { Label = "1 година", Years = 1 },
            new ValidityPeriod { Label = "3 години", Years = 3 },
            new ValidityPeriod { Label = "6 месеца", Months = 6 },
        };

        public static List<ValidityPeriod> CivilLiabilityValidityPeriods = new List<ValidityPeriod>
        {
            new ValidityPeriod { Label = "1 година",  Years = 1 },
            new ValidityPeriod { Label = "6 месеца", Months = 6 },
            new ValidityPeriod { Label = "3 месеца", Months = 3 }
        };

        public static List<ValidityPeriod> CarInsurancePeriodValidityPeriods = new List<ValidityPeriod>
        {
            new ValidityPeriod { Label = "1 година",  Years = 1 },
            new ValidityPeriod { Label = "6 месеца", Months = 6 },
            new ValidityPeriod { Label = "3 месеца", Months = 3 }
        };

        public static List<ValidityPeriod> VignetteValidityPeriods = new List<ValidityPeriod>
        {
            new ValidityPeriod { Label = "1 година",  Years = 1 },
            new ValidityPeriod { Label = "3 месеца", Months = 3 },
            new ValidityPeriod { Label = "1 месец", Months = 1 },
            new ValidityPeriod { Label = "1 седмица", Days = 7 },
        };
    }
}
