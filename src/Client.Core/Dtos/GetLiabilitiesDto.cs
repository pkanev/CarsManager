using System.Collections.Generic;
using Client.Core.Models;

namespace Client.Core.Dtos
{
    public class GetLiabilitiesDto
    {
        public IList<LiabilityExtendedModel> Liabilities { get; set; }
    }
}
