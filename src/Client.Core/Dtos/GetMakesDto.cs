using System.Collections.Generic;
using Client.Core.Models;

namespace Client.Core.Dtos
{
    public class GetMakesDto
    {
        public IList<MakeModel> Makes { get; set; } = new List<MakeModel>();
    }
}
