﻿using System.Collections.Generic;
using CarsManager.Application.Liabilities.Queries.GetLiability;

namespace CarsManager.Application.Liabilities.Queries.GetLiabilities
{
    public class LiabilitiesVm
    {
        public IList<GetLiabilityDto> Liabilities { get; set; }
    }
}
